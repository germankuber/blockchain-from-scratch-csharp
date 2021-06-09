using CSharpFunctionalExtensions;
using MyBlockChain.General;

namespace MyBlockChain.Transactions
{
    public interface ITransactionFactory
    {
        Result<Transaction> Create(Wallet sender,
            Address receiver,
            Amount amount);

        Transaction CreateCoinBase(Address receiver,
            Amount amount);
    }
}