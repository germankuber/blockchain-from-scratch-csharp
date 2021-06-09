using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

namespace MyBlockChain.Transactions
{
    public class TransactionFactory : ITransactionFactory
    {
        private readonly ICalculateInputs _calculateInputs;
        private readonly ICalculateOutputs _calculateOutputs;
        private readonly IScriptBlockFactory _scriptBlockFactory;
        private readonly ITransactionIdStrategy _transactionIdStrategy;
        private readonly IValidateTransaction _validateTransaction;

        public TransactionFactory(IValidateTransaction validateTransaction,
            ITransactionIdStrategy transactionIdStrategy,
            ICalculateInputs calculateInputs,
            ICalculateOutputs calculateOutputs,
            IScriptBlockFactory scriptBlockFactory)
        {
            _validateTransaction = validateTransaction;
            _transactionIdStrategy = transactionIdStrategy;
            _calculateInputs = calculateInputs;
            _calculateOutputs = calculateOutputs;
            _scriptBlockFactory = scriptBlockFactory;
        }

        public Result<Transaction> Create(Wallet sender,
            Address receiver,
            Amount amount)
        {
            return Transaction.NewTransaction(sender,
                    receiver,
                    amount,
                    _calculateInputs,
                    _calculateOutputs,
                    _transactionIdStrategy)
                .Bind(_validateTransaction.Validate);
        }

        public Transaction CreateCoinBase(Address receiver,
            Amount amount)
        {
            return new CoinBaseTransaction(new List<Output> {new(amount, receiver, _scriptBlockFactory) },
                _transactionIdStrategy);
        }
    }
}