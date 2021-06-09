using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace MyBlockChain.Transactions.MemoryPool
{
    public interface IUnconfirmedTransactionPool
    {
        Result<Transaction> AddTransactionToPool(Transaction transaction);
        Maybe<List<Transaction>> GetBestTransactions(int countOfTransactions);
        int TotalTransactions();
    }
}