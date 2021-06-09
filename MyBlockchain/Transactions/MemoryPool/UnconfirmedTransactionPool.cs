using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using MyBlockChain.Blocks;
using MyBlockChain.General;

namespace MyBlockChain.Transactions.MemoryPool
{
    public class UnconfirmedTransactionPool : IUnconfirmedTransactionPool
    {
        private readonly IValidateTransaction _validateTransaction;
        private readonly List<(Amount,Transaction)> _unconfirmedTransaction = new();

        public UnconfirmedTransactionPool(IValidateTransaction validateTransaction)
        {
            _validateTransaction = validateTransaction;
        }
        public Result<Transaction> AddTransactionToPool(Transaction transaction) =>
            Result.FailureIf(
                    _unconfirmedTransaction.Any(x => x.Item2.TransactionId == transaction.TransactionId),
                    transaction,
                    "There is a transaction with that id")
                .Bind(t => _validateTransaction.Validate(t))
                .Tap(t => _unconfirmedTransaction.Add((t.GetTotalFee(), t)));
        public Maybe<List<Transaction>> GetBestTransactions(int countOfTransactions)
        {
            var transactionsToOperate = _unconfirmedTransaction.OrderBy(x=> x.Item1).Take(countOfTransactions);

            //TODO: Remove from Memorypool
            //transactionsToOperate.ToList().ForEach(x => _unconfirmedTransaction.RemoveAt(0));

            return transactionsToOperate.Select(x => x.Item2).ToList()
                .ToMaybe();
        }

        public int TotalTransactions() => _unconfirmedTransaction.Count();
    }
}