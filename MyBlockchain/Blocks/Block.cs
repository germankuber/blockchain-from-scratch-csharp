#region

using System;
using System.Linq;
using MediatR;
using MyBlockChain.General;
using MyBlockChain.Transactions;

#endregion

namespace MyBlockChain.Blocks
{
    public class Block : IRequest<bool>
    {
        public Transactions Transactions;

        private Block(BlockHeader  header,
                      Transactions transactions)
        {
            Transactions = transactions;
            Header       = header;
        }

        public BlockHeader Header { get; }


        public static Block CreateMined(BlockHeader  header,
                                        Transactions transactions)
        {
            return new(header, transactions);
        }

        public static Block Genesis(Address             miner,
                                    ITransactionFactory transactionFactory)
        {
            return new(new BlockHeader(DateTime.Now.TimeOfDay,
                                       "",
                                       string.Concat(Enumerable.Repeat("0", 18)),
                                       "",
                                       1,
                                       1,
                                       new Transactions()),
                       new Transactions(transactionFactory.CreateCoinBase(miner, BlockChainConfig.AmountPerMine)));
        }

        public static Block Genesis()
        {
            return new(new BlockHeader(DateTime.Now.TimeOfDay,
                                       "",
                                       string.Concat(Enumerable.Repeat("0", 18)),
                                       "",
                                       1,
                                       1,
                                       new Transactions()),
                       new Transactions());
        }

        public static Block Mine(Address             miner,
                                 Block               lastBlock,
                                 string              data,
                                 IBlockMineStrategy  strategy,
                                 Transactions        transactions,
                                 ITransactionFactory transactionFactory)
        {
            return strategy.Mine(lastBlock,
                                 data,
                                 data =>
                                     new Block(new BlockHeader(data.TimeSpan,
                                                               data.LastHash,
                                                               data.Hash,
                                                               data.Data,
                                                               data.Nonce,
                                                               data.Difficulty,
                                                               transactions),
                                               GetTransactions(miner, transactions, transactionFactory)));
        }

        private static Transactions GetTransactions(Address             miner, Transactions transactions,
                                                    ITransactionFactory transactionFactory)
        {
            return transactions.AddFirst(
                                         transactionFactory.CreateCoinBase(miner,
                                                                           BlockChainConfig.AmountPerMine
                                                                          +
                                                                           transactions.GetTotalFee()));
        }
    }
}