using System;
using System.Linq;
using MyBlockChain.General;
using MyBlockChain.Transactions;

namespace MyBlockChain.Blocks
{


    public class Block
    {
        private Transactions _transactions;

        private Block(BlockHeader header,
            Transactions transactions)
        {
            _transactions = transactions;
            Header = header;
        }

        public BlockHeader Header { get; }


        private static Block Genesis(Address miner,
            ITransactionFactory transactionFactory) =>
            new(new BlockHeader(DateTime.Now.TimeOfDay,
                    "",
                    string.Concat(Enumerable.Repeat("0", 18)),
                    "",
                    1,
                    1),
                new Transactions(transactionFactory.CreateCoinBase(miner, BlockChainConfig.AmountPerMine)));

        public static Block Genesis() =>
            new(new BlockHeader(DateTime.Now.TimeOfDay,
                    "",
                    string.Concat(Enumerable.Repeat("0", 18)),
                    "",
                    1,
                    1),
                new Transactions());

        public static Block Mine(Address miner,
            IBlockMineStrategy strategy,
            Transactions transactions,
            ITransactionFactory transactionFactory) =>
            strategy.Mine(data =>
                new Block(new BlockHeader(data.TimeSpan,
                        data.LastHash,
                        data.Hash,
                        data.Data,
                        data.Nonce,
                        data.Difficulty),
                    transactions.AddFirst(
                        transactionFactory.CreateCoinBase(miner, BlockChainConfig.AmountPerMine))));
    }
}