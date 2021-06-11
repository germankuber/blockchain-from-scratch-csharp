#region

using System.Collections.Generic;
using MyBlockChain.Blocks;
using MyBlockChain.General;

#endregion

namespace MyBlockChain.Transactions.InputsOutputs
{
    public interface ICalculateOutputs
    {
        public List<Output> Calculate(Wallet     sender, Address receiver, List<Input> inputs, Amount amount,
                                      BlockChain blockChain);
    }
}