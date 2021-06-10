using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CSharpFunctionalExtensions;
using MyBlockChain.General;
using MyBlockChain.Transactions;

namespace MyBlockChain.Blocks
{
    public class Transactions : IEnumerable
    {
        private readonly ImmutableList<Transaction> _transactions = new List<Transaction>().ToImmutableList();

        public Transactions(List<Transaction> transactions)
        {
            _transactions = transactions.ToImmutableList();
        }

        public Transactions()
        {
            _transactions = new List<Transaction>().ToImmutableList();
        }

        public Transactions(Transaction transaction)
        {
            _transactions = _transactions.Add(transaction);
        }

        public IEnumerator GetEnumerator()
        {
            return _transactions.GetEnumerator();
        }

        public Transactions Add(Transaction transaction)
        {
            return new(_transactions.Add(transaction).ToList());
        }

        public Transactions AddFirst(Transaction transaction)
        {
            return new(_transactions.Insert(0, transaction).ToList());
        }

        public Maybe<Transaction> GetTransactionById(TransactionId id)
        {
            return _transactions.TryFirst(x => x.TransactionId == id);
        }

        public Maybe<List<Transaction>> GetAll()
        {
            return _transactions
                .ToList()
                .ToMaybe();
        }

        public Amount GetTotalFee()
        {
            return _transactions
                .ToList()
                .Select(x => x.GetTotalFee())
                .Sum(x => x);
        }

        public Maybe<List<TransactionId>> GetAllTransactionsIds()
        {
            return _transactions.Select(x => x.TransactionId)
                .ToList()
                .ToMaybe();
        }
    }
}