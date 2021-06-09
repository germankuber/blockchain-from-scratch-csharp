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

        public static Block MineGift(Address miner,
            IBlockMineStrategy strategy,
            ITransactionFactory transactionFactory) =>
            strategy.Mine(data =>
                new Block(new BlockHeader(data.TimeSpan,
                        data.LastHash,
                        data.Hash,
                        data.Data,
                        data.Nonce,
                        data.Difficulty),
                    new Transactions(transactionFactory.CreateCoinBase(miner, BlockChainConfig.AmountPerMine))));
    }
}