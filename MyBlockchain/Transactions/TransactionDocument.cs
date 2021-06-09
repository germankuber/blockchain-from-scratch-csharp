using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBlockChain.Transactions
{
    public class TransactionDocument
    {

        [BsonElement("_transactionId")]
        public string TransactionId { get; set; }

        [BsonElement("numberOfTransactionsInput")]
        public int NumberOfTransactionsInput { get; set; }

        [BsonElement("numberOfTransactionsOutputs")]
        public int NumberOfTransactionsOutputs { get; set; }

        [BsonElement("inputs")]
        public List<InputDocument> Inputs { get; set; }

        [BsonElement("outputs")]
        public List<OutputDocument> Outputs { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        public TransactionDocument(Transaction transaction)
        {
            TransactionId = transaction.TransactionId.Hash;
            NumberOfTransactionsInput = transaction.NumberOfTransactionsInput;
            NumberOfTransactionsOutputs = transaction.NumberOfTransactionsOutputs;
            Date = transaction.Date;
            Inputs = transaction.Inputs.Select(x => new InputDocument(x)).ToList();
            Outputs = transaction.Outputs.Select(x => new OutputDocument(x)).ToList();

        }
    }
}