using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MyBlockChain.Transactions;

namespace MyBlockChain.Blocks
{
    public class Transactions
    {
        private readonly ImmutableList<Transaction> _transactions;

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

        public Transactions Add(Transaction transaction)
        {
            return new(_transactions.Add(transaction).ToList());
        }

        public Transactions AddFirst(Transaction transaction)
        {
            return new(_transactions.Insert(0, transaction).ToList());
        }
    }
}