﻿using System.Linq;
using System.Text.Json;

using MyBlockChain.General;

namespace MyBlockChain.Transactions
{
    public class CalculateTransactionIdStrategy : ITransactionIdStrategy
    {
        //TODO: Check how to generate Id
        public TransactionId Calculate(Transaction transaction) =>
            new(HashUtilities.Hash(JsonSerializer.Serialize(new
            {
                Inputs = string.Join('-', transaction.Inputs.Select(x => (string)x.TransactionHash)),
                Outputs = string.Join('-', transaction.Outputs.Select(x => (string)x.Receiver)),
                Date = transaction.Date
            })));
    }
}