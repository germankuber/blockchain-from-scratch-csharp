#region

using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using MyBlockChain.General;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;

#endregion

namespace MyBlockChain.Persistence.Repositories
{
    public class TransactionUtxoUtxoRepository : ITransactionUtxoRepository
    {
        private readonly TransactionsPoolContext _context;
        private readonly IStorageParser          _storageParser;

        public TransactionUtxoUtxoRepository(IStorageParser          storageParser,
                                             TransactionsPoolContext context)
        {
            _storageParser = storageParser;
            _context       = context;
        }


        public void Insert(TransactionWithFee transaction)
        {
            _context.TransactionsUtxo.Add(new TransactionWithFeeDocument(
                                                                         transaction.Transaction, transaction.Fee));
            _context.SaveChanges();
        }

        public void Delete(TransactionWithFee transaction)
        {
            var toRemove = _context.TransactionsUtxo.First(x =>
                                                               x.Id == transaction.Id);
            var toRemove2 = _context.Inputs.Where(x =>
                                                      x.Id == transaction.Id).ToList();
            var toRemove42 = _context.Outputs.Where(x =>
                                                        transaction.Transaction.Outputs.Contains(x)).ToList();

            var toRemove424 = _context.Transactions.Where(x =>
                                                              x.Id == transaction.Id).ToList();
            _context.Remove(toRemove);
            _context.Inputs.RemoveRange(toRemove2);
            _context.Outputs.RemoveRange(toRemove42);
            _context.Transactions.RemoveRange(toRemove424);

            _context.SaveChanges();
        }

        public List<Input> GetInputsByOutputsIds(List<int> outputsIds)
        {
            return _context.Inputs.Where(x => outputsIds.Any(s => x.OutputId == s))
                           .Select(x => _storageParser.Parse(x))
                           .ToList();
        }

        public List<Output> GetOutputsToSpend(Address address)
        {
            return _context.Outputs.Where(x => x.Receiver == address && x.State == OutputStateEnum.UTXO)
                           .Select(x => _storageParser.Parse(x))
                           .ToList();
        }

        public Maybe<Input> GetInputsByOutputId(int outputsId) =>
            _context.Inputs.TryFirst(x => x.OutputId == outputsId)
                    .Select(x => _storageParser.Parse(x));

        public List<TransactionWithFee> GetAllUtxo() =>
            _context.TransactionsUtxo
                    .Select(x => new TransactionWithFee(x.Transaction,
                                                        Amount.Create(x.TotalFee),
                                                        x.Id))
                    .ToList();
    }
}