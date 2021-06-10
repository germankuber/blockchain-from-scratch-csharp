using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyBlockChain.Transactions;

namespace MyBlockChain.Persistence.Dtos
{
    public class TransactionWithFeeDocument
    {
        public TransactionWithFeeDocument(TransactionDocument transaction, int totalFee)
        {
            Transaction = transaction;
            TotalFee = totalFee;
        }

        [BsonElement("_id")] public ObjectId Id { get; set; }

        [BsonElement("totalFee")] public int TotalFee { get; set; }

        [BsonElement("transaction")] public TransactionDocument Transaction { get; set; }
    }
}