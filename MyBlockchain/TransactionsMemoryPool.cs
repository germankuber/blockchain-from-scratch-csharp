using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using MyBlockChain.Transactions;

namespace MyBlockChain
{
    public class TransactionsMemoryPool
    {
        private readonly List<Transaction> _transactions = new();
        private readonly IValidateTransaction _validateTransaction;

        public TransactionsMemoryPool(IValidateTransaction validateTransaction)
        {
            _validateTransaction = validateTransaction;
        }

        public Result<Transaction> AddTransactionToPool(Transaction transaction)
        {
            return _validateTransaction.Validate(transaction)
                .Tap(t => _transactions.Add(t));
        }

        public IReadOnlyCollection<Transaction> GetTransactions(int count)
        {
            return _transactions.Take(count).ToList().AsReadOnly();
        }
    }
}