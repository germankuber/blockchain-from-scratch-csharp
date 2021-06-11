#region

using CSharpFunctionalExtensions;
using MyBlockChain.Blocks;
using MyBlockChain.General;

#endregion

namespace MyBlockChain.Transactions
{
    public interface ITransactionFactory
    {
        Result<Transaction> Create(Wallet     sender,
                                   Address    receiver,
                                   Amount     amount,
                                   BlockChain blockChain);

        Transaction CreateCoinBase(Address receiver,
                                   Amount  amount);
    }
}