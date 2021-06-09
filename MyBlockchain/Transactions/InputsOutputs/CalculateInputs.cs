using System.Collections.Generic;
using MyBlockChain.Blocks;
using MyBlockChain.General;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class CalculateInputs : ICalculateInputs
    {
        private readonly BlockChain _blockChain;

        public CalculateInputs(BlockChain blockChain)
        {
            _blockChain = blockChain;
        }

        public List<Input> GetEnoughInputsFor(Amount amount)
        {
            return default;
        }
    }
}