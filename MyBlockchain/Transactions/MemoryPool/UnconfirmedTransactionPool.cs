#region

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CSharpFunctionalExtensions;
using MyBlockChain.Blocks;
using MyBlockChain.Persistence;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;

#endregion

namespace MyBlockChain.Transactions.MemoryPool
{
    public class UnconfirmedTransactionPool : IUnconfirmedTransactionPool
    {
        private readonly IOutputsRepository         _outputsRepository;
        private readonly IStorageParser             _storageParser;
        private readonly ITransactionUtxoRepository _transactionUtxoRepository;
        private readonly IValidateTransaction       _validateTransaction;

        public UnconfirmedTransactionPool(IValidateTransaction       validateTransaction,
                                          ITransactionUtxoRepository transactionUtxoRepository,
                                          IStorageParser             storageParser,
                                          IOutputsRepository         outputsRepository)
        {
            _validateTransaction       = validateTransaction;
            _transactionUtxoRepository = transactionUtxoRepository;
            _storageParser             = storageParser;
            _outputsRepository         = outputsRepository;
        }

        public Result<Transaction> AddTransactionToPool(Transaction transaction)
        {
            return Result.FailureIf(
                                    _transactionUtxoRepository.GetAllUtxo()
                                                              .Any(x => x.Transaction.TransactionId ==
                                                                        transaction.TransactionId.Hash),
                                    transaction,
                                    "There is a transaction with that id")
                         .Bind(t => _validateTransaction.Validate(t))
                         .Tap(t => _transactionUtxoRepository.Insert(new TransactionWithFee(_storageParser.Parse(t),
                                                                                            t.GetTotalFee())));
        }

        public Maybe<List<Transaction>> GetBestTransactions(int countOfTransactions)
        {
            var transactionsToOperate = _transactionUtxoRepository.GetAllUtxo().OrderBy(x => x.Fee)
                                                                  .Take(countOfTransactions)
                                                                  .ToImmutableList();

            transactionsToOperate.ForEach(x => _transactionUtxoRepository.Delete(x));
            return transactionsToOperate.Select(x => _storageParser.Parse(x.Transaction, _outputsRepository)).ToList()
                                        .ToMaybe();
        }

        public int TotalTransactions()
        {
            return _transactionUtxoRepository.GetAllUtxo().Count();
        }
    }
}