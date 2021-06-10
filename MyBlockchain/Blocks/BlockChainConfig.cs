using System;
using MyBlockChain.General;

namespace MyBlockChain.Blocks
{
    public static class BlockChainConfig
    {
        private static readonly TimeSpan TimeToMined = TimeSpan.FromSeconds(5);
        private static readonly int MaxDifficulty = 4;
        private static readonly int MinDifficulty = 0;
        public static readonly int MaxTransactionsPerBlock = 2;
        private static int _difficulty;
        public static readonly Amount AmountPerMine = Amount.Create(5);
        public static readonly Amount FeePerTransaction = Amount.Create(1);

        public static void ActualMineTime(TimeSpan time)
        {
            if (time > TimeToMined && _difficulty != MinDifficulty)
                --_difficulty;
            if (time < TimeToMined && _difficulty != MaxDifficulty)
                ++_difficulty;
        }

        public static int GetActualDifficulty()
        {
            return _difficulty;
        }
    }
}