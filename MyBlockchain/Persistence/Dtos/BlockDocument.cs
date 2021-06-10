using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyBlockChain.Blocks;
using MyBlockChain.Transactions;

namespace MyBlockChain.Persistence.Dtos
{
    public class BlockDocument
    {
        public BlockDocument(Block block)
        {
            Header = new BlockHeaderDocument(block.Header);
            block.Transactions.GetAll()
                .ToResult("").Map(x => x.Select(t => new TransactionDocument(t)))
                .Tap(x => Transactions = x.ToList());
        }

        [BsonElement("_id")] public ObjectId Id { get; set; }

        [BsonElement("header")] public BlockHeaderDocument Header { get; set; }

        [BsonElement("transactions")] public List<TransactionDocument> Transactions { get; set; }
    }
}