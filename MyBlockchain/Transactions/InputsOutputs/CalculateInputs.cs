using System.Collections.Generic;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Repositories.Interfaces;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class CalculateInputs : ICalculateInputs
    {
        private readonly IOutputsRepository _outputsRepository;
        private readonly IBlockRepository _blockRepository;

        public CalculateInputs(IOutputsRepository outputsRepository, IBlockRepository blockRepository)
        {
            _outputsRepository = outputsRepository;
            _blockRepository = blockRepository;
        }

        public List<Input> GetEnoughInputsFor(Wallet sender, Amount amount, BlockChain blockChain)
        {
            //TODO: RefactorizaR
            var transactionsOutputsToReturn = new List<Input>();
            var totalAmountInOutputs = Amount.Create(0);
            var blocks = _blockRepository.GetWithTransactionsToSpent(blockChain ,sender.Address, amount);
            foreach (var blockChainBlock in blocks)
                foreach (Transaction transaction in blockChainBlock.Transactions)
                {
                    var position = 0;
                    foreach (var transactionOutput in transaction.Outputs)
                        if (transactionOutput.State == OutputStateEnum.UTXO)
                        {
                            //TODO: checkear que tengo que firmar
                            transactionsOutputsToReturn.Add(new Input(position, transaction.TransactionId,
                                SignatureMessage.Sign(sender.PrivateKey)));
                            totalAmountInOutputs += transactionOutput.Amount;
                            transactionOutput.Spend(sender.PrivateKey);
                            if (totalAmountInOutputs >= amount)
                                return transactionsOutputsToReturn;
                        }
                }

            return transactionsOutputsToReturn;
        }
    }
}