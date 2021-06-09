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

        public List<Input> GetEnoughInputsFor(Wallet sender, Amount amount)
        {
            //TODO: RefactorizaR
            var transactionsOutputsToReturn = new List<Input>();
            var totalAmountInOutputs = Amount.Create(0);
            foreach (var blockChainBlock in _blockChain.Blocks)
            {
                foreach (Transaction transaction in blockChainBlock.Transactions)
                {
                    int position = 0;
                    foreach (var transactionOutput in transaction.Outputs)
                    {
                        if (transactionOutput.State == OutputStateEnum.UTXO)
                        {
                            //TODO: checkear que tengo que firmar
                            transactionsOutputsToReturn.Add(new Input(position, transaction.TransactionId, SignatureMessage.Sign(sender.PrivateKey)));
                            totalAmountInOutputs += transactionOutput.Amount;
                            if (totalAmountInOutputs >= amount)
                                return transactionsOutputsToReturn;
                        }
                    }
                }
            }

            return transactionsOutputsToReturn;
        }
    }
}