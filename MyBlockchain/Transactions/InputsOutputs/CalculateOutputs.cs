using System.Collections.Generic;
using MyBlockChain.Blocks;
using MyBlockChain.General;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class CalculateOutputs : ICalculateOutputs
    {
        private readonly BlockChain _blockChain;

        public CalculateOutputs(BlockChain blockChain)
        {
            _blockChain = blockChain;
        }

        public List<Output> Calculate(List<Input> inputs, Amount amount)
        {
            return default;
        }
    }
}