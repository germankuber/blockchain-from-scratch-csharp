using System;

using MongoDB.Bson;
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

        public OutputDocument()
        {
            
        }
        public int Id { get; set; }
        public TransactionDocument TransactionDocument { get; set; }
        public OutputStateEnum State { get; set; } = OutputStateEnum.UTXO;

        public int Amount { get; set; }

        public string Receiver { get; set; }
    }
}