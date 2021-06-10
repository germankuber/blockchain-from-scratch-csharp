using System;

using MongoDB.Bson.Serialization.Attributes;

namespace MyBlockChain.Blocks
{
    public class BlockHeaderDocument
    {
        public BlockHeaderDocument(BlockHeader blockHeader)
        {
            TimeSpan = blockHeader.TimeSpan;
            LastHash = blockHeader.LastHash;
            Hash = blockHeader.Hash;
            Data = blockHeader.Data;
            Nonce = blockHeader.Nonce;
            Difficulty = blockHeader.Difficulty;
            MerkleRoot = blockHeader.MerkleRoot ?? "";
        }

        public BlockHeaderDocument()
        {

        }

        public int Id { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string LastHash { get; }
        public string Hash { get; set; }
        public string Data { get; set; }
        public int Nonce { get; set; }
        public int Difficulty { get; set; }
        public string MerkleRoot { get; set; }
    }
}