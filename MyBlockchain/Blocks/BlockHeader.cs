using System;

namespace MyBlockChain.Blocks
{
    public class BlockHeader
    {
        public BlockHeader(TimeSpan timeSpan, string lastHash, string hash, string data, int nonce, int difficulty)
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