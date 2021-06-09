﻿using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Microsoft.VisualBasic;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Transactions
{
    public class Transaction
    {
        private readonly ITransactionIdStrategy _transactionIdStrategy;
        private static BlockChain _blockChain;

        protected Transaction(List<Input> inputs,
            List<Output> outputs,
            ITransactionIdStrategy transactionIdStrategy)
        {
            _transactionIdStrategy = transactionIdStrategy;

            Inputs = inputs;
            Outputs = outputs;
            TransactionId = transactionIdStrategy.Calculate(this);
        }

        public TransactionId TransactionId { get; set; }
        public int NumberOfTransactionsInput { get; set; }
        public int NumberOfTransactionsOutputs { get; set; }
        public List<Input> Inputs { get; set; }
        public List<Output> Outputs { get; set; }
        public Amount Amount { get; set; }
        
        public DateTime Date => DateAndTime.Now;

        public static Result<Transaction> NewTransaction(Wallet sender,
            Address receiver,
            Amount amount,
            ICalculateInputs calculateInputs,
            ICalculateOutputs calculateOutputs,
            ITransactionIdStrategy transactionIdStrategy, 
            BlockChain blockChain)
        {
            _blockChain = blockChain;
            if (sender.GetBalance() <= amount)
                return Result.Failure<Transaction>("You do not have enough money");

            var inputs = calculateInputs.GetEnoughInputsFor(sender,amount);
            return new Transaction(inputs,
                calculateOutputs.Calculate(sender, receiver,inputs, amount),
                transactionIdStrategy);
        }

        public Amount GetTotalFee()
        {
            var inputTotalAmount = Inputs.GetTotalAmount(_blockChain);
            var outputTotalAmount = Outputs.GetTotalAmount();

            return inputTotalAmount - outputTotalAmount;
        }

       
    }
}