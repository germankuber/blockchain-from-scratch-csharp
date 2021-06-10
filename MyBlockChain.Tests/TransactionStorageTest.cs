using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MyBlockChain.Persistence;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;

namespace MyBlockChain.Tests
{
    public class TransactionStorageTest : ITransactionStorage
    {
        private readonly List<TransactionWithFee> _list = new();

        public void Insert(TransactionWithFee transaction)
        {
            _list.Add(transaction);
        }

        public List<TransactionWithFee> GetAllUtxo()
        {
            return _list;
        }

        public void Delete(Transaction transaction)
        {
            _list.TryFirst(x => x.Transaction.TransactionId.Hash == transaction.TransactionId.Hash)
                .Execute(x => _list.Remove(x));
        }
    }
}