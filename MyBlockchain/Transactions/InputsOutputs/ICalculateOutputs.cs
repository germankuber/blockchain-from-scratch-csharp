using System.Collections.Generic;
using MyBlockChain.General;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public interface ICalculateOutputs
    {
        public List<Output> Calculate(Wallet sender, Address receiver, List<Input> inputs, Amount amount);
    }
}