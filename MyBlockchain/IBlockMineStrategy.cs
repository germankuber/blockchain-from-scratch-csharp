using System;
using MyBlockChain.Blocks;

namespace MyBlockChain
{
    public interface IBlockMineStrategy
    {
        Block Mine(Func<BlockData, Block> createBlock);
        string GetBlockHash(Block block);

        public class BlockData
        {
            public BlockData(TimeSpan timeSpan, string lastHash, string hash, string data, int nonce, int difficulty)
            {
                TimeSpan = timeSpan;
                LastHash = lastHash;
                Hash = hash;
                Data = data;
                Nonce = nonce;
                Difficulty = difficulty;
            }

            public TimeSpan TimeSpan { get; }
            public string LastHash { get; }
            public string Hash { get; }
            public string Data { get; }
            public int Nonce { get; }
            public int Difficulty { get; }
        }
    }
}