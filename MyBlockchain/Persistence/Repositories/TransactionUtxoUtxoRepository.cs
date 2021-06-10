using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MyBlockChain.General;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;

namespace MyBlockChain.Persistence.Repositories
{
    public class TransactionUtxoUtxoRepository : ITransactionUtxoRepository
    {
        private readonly IStorageParser _storageParser;
        private readonly TransactionsPoolContext _context;

        public TransactionUtxoUtxoRepository(IStorageParser storageParser,
            TransactionsPoolContext  context)
        {
            _storageParser = storageParser;
            _context = context;

        }


        public void Insert(TransactionWithFee transaction)
        {
            _context.TransactionsUtxo.Add(new TransactionWithFeeDocument(
                _storageParser.Parse(transaction.Transaction), transaction.Fee));
            _context.SaveChanges();
        }

        public void Delete(TransactionWithFee transaction)
        {
            var toRemove = _context.TransactionsUtxo.First(x =>
                x.Id == transaction.Id);
            toRemove.Spend = true;
            _context.SaveChanges();
        }

        public List<TransactionWithFee> GetAllUtxo() =>
            _context.TransactionsUtxo
                .Select(x => new TransactionWithFee(_storageParser.Parse(x.Transaction),
                    Amount.Create(x.TotalFee),
                    x.Id))
                .ToList();

    
    }
}