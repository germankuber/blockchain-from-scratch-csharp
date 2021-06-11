#region

using System.Collections.Generic;
using System.Linq;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

#endregion

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class CalculateOutputs : ICalculateOutputs
    {
        private readonly IFeeCalculation     _feeCalculation;
        private readonly IOutputsRepository  _outputsRepository;
        private readonly IScriptBlockFactory _scriptBlockFactory;

        public CalculateOutputs(IScriptBlockFactory scriptBlockFactory,
                                IOutputsRepository  outputsRepository,
                                IFeeCalculation     feeCalculation)
        {
            _scriptBlockFactory = scriptBlockFactory;
            _outputsRepository  = outputsRepository;
            _feeCalculation     = feeCalculation;
        }

        public List<Output> Calculate(Wallet     sender, Address receiver, List<Input> inputs, Amount amount,
                                      BlockChain blockChain)
        {
            var outputs = new List<Output>();

            var totalToSpend = (Amount) inputs.Select(input =>
                                                      {
                                                          var currentTransactionOutput =
                                                              _outputsRepository.GetById(input.OutputId);
                                                          currentTransactionOutput.Spend(sender.PrivateKey);
                                                          return currentTransactionOutput;
                                                      }).Sum(x => x.Amount);
            outputs.Add(new Output(amount, receiver, _scriptBlockFactory));
            var fee = _feeCalculation.GetFee();
            if (totalToSpend > amount + fee)
                outputs.Add(new Output(totalToSpend - amount - fee,
                                       sender.Address,
                                       _scriptBlockFactory));


            return outputs;
        }
    }
}