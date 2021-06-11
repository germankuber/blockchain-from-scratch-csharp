#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CSharpFunctionalExtensions;
using Microsoft.VisualBasic;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions.InputsOutputs;

#endregion

namespace MyBlockChain.Transactions
{
    public class Transaction
    {
        private readonly ITransactionIdStrategy _transactionIdStrategy;
        private readonly IOutputsRepository     _outputsRepository;

        protected Transaction(List<Input>            inputs,
                              List<Output>           outputs,
                              ITransactionIdStrategy transactionIdStrategy,
                              TransactionId?         transactionId,
                              IOutputsRepository     outputsRepository)
        {
            _transactionIdStrategy = transactionIdStrategy;
            _outputsRepository     = outputsRepository;

            AddInputs(inputs);
            AddOutputs(outputs);
            TransactionId = transactionIdStrategy.Calculate(this);
            TransactionId = transactionId;
        }
        //protected Transaction(List<Input> inputs,
        //    List<Output> outputs,
        //    ITransactionIdStrategy transactionIdStrategy,
        //    IOutputsRepository outputsRepository)
        //{
        //    _transactionIdStrategy = transactionIdStrategy;

        //    AddInputs(inputs);
        //    AddOutputs(outputs);
        //    TransactionId = transactionIdStrategy.Calculate(this);
        //}
        protected Transaction(List<Input>            inputs,
                              List<Output>           outputs,
                              ITransactionIdStrategy transactionIdStrategy,
                              IOutputsRepository     outputsRepository)
        {
            _transactionIdStrategy = transactionIdStrategy;
            _outputsRepository     = outputsRepository;

            AddInputs(inputs);
            AddOutputs(outputs);
            TransactionId = transactionIdStrategy.Calculate(this);
        }

        [ForeignKey("TransactionWithFeeId")] public int TransactionWithFeeId { get; set; }

        public TransactionWithFee TransactionWithFee { get; set; }

        public TransactionId TransactionId               { get; set; }
        public int           NumberOfTransactionsInput   { get; set; }
        public int           NumberOfTransactionsOutputs { get; set; }
        public List<Input>   Inputs                      { get; private set; }
        public List<Output>  Outputs                     { get; private set; }

        public DateTime Date => DateAndTime.Now;

        public static Transaction CreateEmpty(TransactionId          transactionId,
                                              List<Input>            inputs,
                                              List<Output>           outputs,
                                              ITransactionIdStrategy transactionIdStrategy,
                                              IOutputsRepository     outputsRepository)
        {
            return new(inputs, outputs, transactionIdStrategy, transactionId, outputsRepository);
        }

        public static Result<Transaction> NewTransaction(Wallet                 sender,
                                                         Address                receiver,
                                                         Amount                 amount,
                                                         ICalculateInputs       calculateInputs,
                                                         ICalculateOutputs      calculateOutputs,
                                                         ITransactionIdStrategy transactionIdStrategy,
                                                         BlockChain             blockChain,
                                                         IOutputsRepository     outputsRepository)
        {
            if (sender.GetBalance() <= amount)
                return Result.Failure<Transaction>("You do not have enough money");

            var inputs = calculateInputs.GetEnoughInputsFor(sender, amount, blockChain);
            return new Transaction(inputs,
                                   calculateOutputs.Calculate(sender, receiver, inputs, amount, blockChain),
                                   transactionIdStrategy,
                                   outputsRepository);
        }

        public Amount GetTotalFee()
        {
            var zero = Amount.Create(0);
            Amount inputTotalAmount =
                _outputsRepository != null
                    ? _outputsRepository.GetBalance(Inputs)
                    : zero;

            var outputTotalAmount = Outputs.GetTotalAmount();
            if ((inputTotalAmount - outputTotalAmount) <= Amount.Create(0))
                throw new Exception("Does not have money");
            return inputTotalAmount == zero
                       ? zero
                       : inputTotalAmount - outputTotalAmount;
        }

        private void AddInputs(List<Input> inputs)
        {
            NumberOfTransactionsInput = inputs.Count;
            Inputs                    = inputs;
        }

        private void AddOutputs(List<Output> outputs)
        {
            NumberOfTransactionsOutputs = outputs.Count;
            Outputs                     = outputs;
        }
    }
}