using System.Collections.Generic;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Transactions
{
    public class CoinBaseTransaction : Transaction
    {
        public CoinBaseTransaction(List<Output> outputs,
            ITransactionIdStrategy transactionIdStrategy)
            : base(new List<Input> {new CoinBase("")}, outputs, transactionIdStrategy)
        {
        }
    }
}