#region

using System.Collections.Generic;
using MyBlockChain.Blocks;
using MyBlockChain.General;

#endregion

namespace MyBlockChain.Transactions.InputsOutputs
{
    public interface ICalculateInputs
    {
        public List<Input> GetEnoughInputsFor(Wallet sender, Amount amount, BlockChain blockChain);
    }
}