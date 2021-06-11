#region

using System;
using System.Linq;
using CSharpFunctionalExtensions;
using MyBlockChain.General;

#endregion

namespace MyBlockChain.Blocks
{
    public class BlockHeader
    {
        public BlockHeader(TimeSpan     timeSpan,
                           string       lastHash,
                           string       hash,
                           string       data,
                           int          nonce,
                           int          difficulty,
                           Transactions transactions)
        {
            TimeSpan   = timeSpan;
            LastHash   = lastHash;
            Hash       = hash;
            Data       = data;
            Nonce      = nonce;
            Difficulty = difficulty;
            transactions.GetAllTransactionsIds()
                        .Execute(t =>
                                 {
                                     MerkleRoot = new Hash(merkleroot.MerkleRoot.merkle(
                                                                                        t.Select(x => (string) x.Hash)
                                                                                         .ToArray()));
                                 });
        }

        public TimeSpan TimeSpan   { get; }
        public string   LastHash   { get; }
        public string   Hash       { get; }
        public string   Data       { get; }
        public int      Nonce      { get; }
        public int      Difficulty { get; }
        public Hash     MerkleRoot { get; set; }
    }
}