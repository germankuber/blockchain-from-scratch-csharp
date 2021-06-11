#region

using System.Collections.Generic;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions.InputsOutputs;

#endregion

namespace MyBlockChain.Transactions
{
    public class CoinBaseTransaction : Transaction
    {
        public CoinBaseTransaction(List<Output>           outputs,
                                   ITransactionIdStrategy transactionIdStrategy,
                                   IOutputsRepository     outputsRepository)
            : base(new List<Input> {new CoinBase("")}, outputs, transactionIdStrategy, outputsRepository)
        {
        }
    }
}