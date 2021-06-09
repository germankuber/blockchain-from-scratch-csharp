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
            Nonce=blockHeader.Nonce;
            Difficulty = blockHeader.Difficulty;
            MerkleRoot = blockHeader.MerkleRoot ?? "";
        }
        public TimeSpan TimeSpan { get; set; }
        public string LastHash { get; }
        [BsonElement("_hash")]
        public string Hash { get; set; }
        [BsonElement("data")]
        public string Data { get; set; }
        [BsonElement("nonce")]
        public int Nonce { get; set; }
        [BsonElement("difficulty")]
        public int Difficulty { get; set; }
        [BsonElement("merkleRoot")]
        public string MerkleRoot { get; set; }

    }
}