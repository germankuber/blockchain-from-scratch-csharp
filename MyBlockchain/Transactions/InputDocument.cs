using MongoDB.Bson.Serialization.Attributes;

using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Transactions
{
    public class InputDocument
    {
        public InputDocument(Input input)
        {
            TransactionOutputPosition = input.TransactionOutputPosition;
            TransactionHash = input.TransactionHash;
            Signature = input.Signature;
        }

        public InputDocument()
        {
            
        }
        public int Id { get; set; }
        public TransactionDocument TransactionDocument { get; set; }
        public string TransactionHash { get; set; }

        public int TransactionOutputPosition { get; set; }

        public string Signature { get; set; }
    }
}