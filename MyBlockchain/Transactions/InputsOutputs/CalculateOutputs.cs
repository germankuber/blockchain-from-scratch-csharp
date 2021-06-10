using System.Collections.Generic;
using System.Linq;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class CalculateOutputs : ICalculateOutputs
    {
        private readonly IFeeCalculation _feeCalculation;
        private readonly IScriptBlockFactory _scriptBlockFactory;

        public CalculateOutputs(IScriptBlockFactory scriptBlockFactory,
            IFeeCalculation feeCalculation)
        {
            _scriptBlockFactory = scriptBlockFactory;
            _feeCalculation = feeCalculation;
        }

        public List<Output> Calculate(Wallet sender, Address receiver, List<Input> inputs, Amount amount,
            BlockChain blockChain)
        {
            var outputs = new List<Output>();

            var totalToSpend = (Amount) inputs.Select(input =>
            {
                var currentTransactionOutput =
                    blockChain.GetOutputByTransactionIdAndPosition(new TransactionId(input.TransactionHash),
                            input.TransactionOutputPosition)
                        .Value;
                currentTransactionOutput.Spend(sender.PrivateKey);
                return currentTransactionOutput;
            }).Sum(x => x.Amount);
            outputs.Add(new Output(amount, receiver, _scriptBlockFactory));
            var fee = _feeCalculation.GetFee();
            if (totalToSpend > amount + fee)
            {
                //TODO: check how to get the fee
                //Check what happen with the fee difference
                //var outputToReturnExtraMoney = totalToSpend - amount - fee;
                var outputToReturnExtraMoney = totalToSpend - amount - fee;

                outputs.Add(new Output(outputToReturnExtraMoney, sender.Address, _scriptBlockFactory));
            }


            return outputs;
        }
    }
}