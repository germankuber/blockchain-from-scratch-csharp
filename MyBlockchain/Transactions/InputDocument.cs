using MongoDB.Bson.Serialization.Attributes;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Transactions
{
    public class InputDocument
    {
        [BsonElement("transactionHash")]
        public string TransactionHash { get; set; }
        [BsonElement("transactionOutputPosition")]
        public int TransactionOutputPosition { get; set; }
        [BsonElement("signature")]
        public string Signature { get; set; }

        public InputDocument(Input input)
        {
            TransactionOutputPosition = input.TransactionOutputPosition;
            TransactionHash = input.TransactionHash;
            Signature = input.Signature;
        }
    }
}