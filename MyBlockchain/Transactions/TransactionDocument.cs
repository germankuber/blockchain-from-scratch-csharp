#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace MyBlockChain.Transactions
{
    public class TransactionDocument
    {
        public TransactionDocument(Transaction transaction)
        {
            TransactionId               = transaction.TransactionId.Hash;
            NumberOfTransactionsInput   = transaction.NumberOfTransactionsInput;
            NumberOfTransactionsOutputs = transaction.NumberOfTransactionsOutputs;
            Date                        = transaction.Date;
            Inputs                      = transaction.Inputs.Select(x => new InputDocument(x)).ToList();
            Outputs                     = transaction.Outputs.Select(x => new OutputDocument(x)).ToList();
        }

        public TransactionDocument()
        {
        }

        public int    Id            { get; set; }
        public string TransactionId { get; set; }

        public int NumberOfTransactionsInput { get; set; }

        public int NumberOfTransactionsOutputs { get; set; }

        public List<InputDocument> Inputs { get; set; }

        public List<OutputDocument> Outputs { get; set; }

        public DateTime Date { get; set; }
    }
}