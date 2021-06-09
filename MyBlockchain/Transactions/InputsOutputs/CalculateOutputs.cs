using System.Collections.Generic;
using System.Linq;

using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class CalculateOutputs : ICalculateOutputs
    {
        private readonly BlockChain _blockChain;
        private readonly IScriptBlockFactory _scriptBlockFactory;
        private readonly IFeeCalculation _feeCalculation;

        public CalculateOutputs(BlockChain blockChain,
            IScriptBlockFactory scriptBlockFactory,
            IFeeCalculation feeCalculation)
        {
            _blockChain = blockChain;
            _scriptBlockFactory = scriptBlockFactory;
            _feeCalculation = feeCalculation;
        }

        public List<Output> Calculate(Wallet sender, Address receiver, List<Input> inputs, Amount amount)
        {
            var outputs = new List<Output>();

            var totalToSpend = (Amount)inputs.Select(input =>
            {
                var currentTransactionOutput =
                    _blockChain.GetOutputByTransactionIdAndPosition(new TransactionId(input.TransactionHash),
                            input.TransactionOutputPosition)
                        .Value;
                currentTransactionOutput.Spend(sender.PrivateKey);
                return currentTransactionOutput;
            }).Sum(x => x.Amount);
            outputs.Add(new Output(amount, receiver, _scriptBlockFactory));

            if (totalToSpend > amount)
            {
                var outputToReturnExtraMoney = totalToSpend - amount;
                outputs.Add(new Output(outputToReturnExtraMoney, sender.Address, _scriptBlockFactory));
            }


            return outputs;
        }
    }
}