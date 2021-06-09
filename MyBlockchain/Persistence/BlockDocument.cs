using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using LevelDB;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyBlockChain.Blocks;
using MyBlockChain.Transactions;

namespace MyBlockChain.Persistence
{
    public class WalletDocument
    {

        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("privateKey")]
        public string PrivateKey { get; set; }

        public WalletDocument(Wallet wallet)
        {
            Address = wallet.Address;
            PrivateKey = wallet.PrivateKey;
        }
    }
    public class BlockDocument
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("header")]
        public BlockHeaderDocument Header { get; set; }
        [BsonElement("transactions")]

        public List<TransactionDocument> Transactions { get; set; }

        public BlockDocument(Block block)
        {
            Header = new BlockHeaderDocument(block.Header);
            block.Transactions.GetAll()
                .ToResult("").Map(x => x.Select(t => new TransactionDocument(t)))
                .Tap(x => Transactions = x.ToList());
        }

    }
}
