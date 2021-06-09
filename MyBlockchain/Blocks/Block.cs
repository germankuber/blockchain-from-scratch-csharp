using System;
using System.Linq;

using MyBlockChain.General;
using MyBlockChain.Transactions;

namespace MyBlockChain.Blocks
{


    public class Block
    {
        public Transactions Transactions;

        private Block(BlockHeader header,
            Transactions transactions)
        {
            Transactions = transactions;
            Header = header;
        }

        public BlockHeader Header { get; }


        public static Block Genesis(Address miner,
            ITransactionFactory transactionFactory) =>
            new(new BlockHeader(DateTime.Now.TimeOfDay,
                    "",
                    string.Concat(Enumerable.Repeat("0", 18)),
                    "",
                    1,
                    1,
                    new Transactions()),
                new Transactions(transactionFactory.CreateCoinBase(miner, BlockChainConfig.AmountPerMine)));

        public static Block Genesis() =>
            new(new BlockHeader(DateTime.Now.TimeOfDay,
                    "",
                    string.Concat(Enumerable.Repeat("0", 18)),
                    "",
                    1,
                    1,
                    new Transactions()),
                new Transactions());

        public static Block Mine(Address miner,
            IBlockMineStrategy strategy,
            Transactions transactions,
            ITransactionFactory transactionFactory)
        {
           var transactionsNew= transactions.AddFirst(
                transactionFactory.CreateCoinBase(miner, BlockChainConfig.AmountPerMine));
            return strategy.Mine(data =>
                new Block(new BlockHeader(data.TimeSpan,
                        data.LastHash,
                        data.Hash,
                        data.Data,
                        data.Nonce,
                        data.Difficulty,
                        transactions),
                    transactionsNew));
        }

        public static Block MineGift(Address miner,
            IBlockMineStrategy strategy,
            ITransactionFactory transactionFactory)
        {
            var transactions =
                new Transactions(transactionFactory.CreateCoinBase(miner, BlockChainConfig.AmountPerMine));
            return strategy.Mine(data =>
                new Block(new BlockHeader(data.TimeSpan,
                        data.LastHash,
                        data.Hash,
                        data.Data,
                        data.Nonce,
                        data.Difficulty,
                        transactions),
                    transactions));
        }
    }
}