using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CSharpFunctionalExtensions;
using MyBlockChain.Blocks;
using MyBlockChain.Persistence;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;

namespace MyBlockChain.Transactions.MemoryPool
{
    public class UnconfirmedTransactionPool : IUnconfirmedTransactionPool
    {
        private readonly ITransactionStorage _transactionStorage;
        private readonly IValidateTransaction _validateTransaction;

        public UnconfirmedTransactionPool(IValidateTransaction validateTransaction,
            ITransactionStorage transactionStorage)
        {
            _validateTransaction = validateTransaction;
            _transactionStorage = transactionStorage;
        }

        public Result<Transaction> AddTransactionToPool(Transaction transaction)
        {
            return Result.FailureIf(
                    _transactionStorage.GetAllUtxo().Any(x => x.Transaction.TransactionId == transaction.TransactionId),
                    transaction,
                    "There is a transaction with that id")
                .Bind(t => _validateTransaction.Validate(t))
                .Tap(t => _transactionStorage.Insert(new TransactionWithFee(t, t.GetTotalFee())));
        }

        public Maybe<List<Transaction>> GetBestTransactions(int countOfTransactions)
        {
            var transactionsToOperate = _transactionStorage.GetAllUtxo().OrderBy(x => x.Fee)
                .Take(countOfTransactions)
                .Select(x => x.Transaction)
                .ToImmutableList();

            transactionsToOperate.ForEach(x => _transactionStorage.Delete(x));
            return transactionsToOperate.ToList().ToMaybe();
        }

        public int TotalTransactions()
        {
            return _transactionStorage.GetAllUtxo().Count();
        }
    }
}