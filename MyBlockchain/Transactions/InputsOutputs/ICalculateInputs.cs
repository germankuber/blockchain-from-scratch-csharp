using System.Collections.Generic;
using MyBlockChain.General;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public interface ICalculateInputs
    {
        public List<Input> GetEnoughInputsFor(Wallet sender,Amount amount);
    }
}