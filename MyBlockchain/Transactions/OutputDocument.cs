using MongoDB.Bson.Serialization.Attributes;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Transactions
{
    public class OutputDocument
    {
        public OutputDocument(Output output)
        {
            Amount = output.Amount;
            Receiver = output.Receiver;
            State = output.State;
        }

        [BsonElement("state")] public OutputStateEnum State { get; set; } = OutputStateEnum.UTXO;

        [BsonElement("amount")] public int Amount { get; set; }

        [BsonElement("receiver")] public string Receiver { get; set; }
    }
}