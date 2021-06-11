#region

using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

#endregion

namespace MyBlockChain.Transactions
{
    public class TransactionFactory : ITransactionFactory
    {
        private readonly ICalculateInputs       _calculateInputs;
        private readonly ICalculateOutputs      _calculateOutputs;
        private readonly IOutputsRepository     _outputsRepository;
        private readonly IScriptBlockFactory    _scriptBlockFactory;
        private readonly ITransactionIdStrategy _transactionIdStrategy;
        private readonly IValidateTransaction   _validateTransaction;

        public TransactionFactory(IValidateTransaction   validateTransaction,
                                  ITransactionIdStrategy transactionIdStrategy,
                                  ICalculateInputs       calculateInputs,
                                  ICalculateOutputs      calculateOutputs,
                                  IScriptBlockFactory    scriptBlockFactory,
                                  IOutputsRepository     outputsRepository)
        {
            _validateTransaction   = validateTransaction;
            _transactionIdStrategy = transactionIdStrategy;
            _calculateInputs       = calculateInputs;
            _calculateOutputs      = calculateOutputs;
            _scriptBlockFactory    = scriptBlockFactory;
            _outputsRepository     = outputsRepository;
        }

        public Result<Transaction> Create(Wallet     sender,
                                          Address    receiver,
                                          Amount     amount,
                                          BlockChain blockChain)
        {
            return Transaction.NewTransaction(sender,
                                              receiver,
                                              amount,
                                              _calculateInputs,
                                              _calculateOutputs,
                                              _transactionIdStrategy,
                                              blockChain,
                                              _outputsRepository)
                              .Bind(_validateTransaction.Validate);
        }

        public Transaction CreateCoinBase(Address receiver,
                                          Amount  amount)
        {
            return new CoinBaseTransaction(new List<Output> {new(amount, receiver, _scriptBlockFactory)},
                                           _transactionIdStrategy, _outputsRepository);
        }
    }
}