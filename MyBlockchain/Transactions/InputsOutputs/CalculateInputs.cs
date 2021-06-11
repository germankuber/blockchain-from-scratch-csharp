#region

using System;
using System.Collections.Generic;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Repositories.Interfaces;

#endregion

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class CalculateInputs : ICalculateInputs
    {
        private readonly IBlockRepository           _blockRepository;
        private readonly IOutputsRepository         _outputsRepository;
        private readonly ITransactionUtxoRepository _transactionUtxoRepository;

        public CalculateInputs(IOutputsRepository         outputsRepository,
                               ITransactionUtxoRepository transactionUtxoRepository,
                               IBlockRepository           blockRepository)
        {
            _outputsRepository         = outputsRepository;
            _transactionUtxoRepository = transactionUtxoRepository;
            _blockRepository           = blockRepository;
        }

        public List<Input> GetEnoughInputsFor(Wallet sender, Amount amount, BlockChain blockChain)
        {
            //TODO: RefactorizaR
            var transactionsOutputsToReturn = new List<Input>();
            var totalAmountInOutputs = Amount.Create(0);
            var blocks = _blockRepository.GetWithTransactionsToSpent(blockChain, sender.Address, amount);
            foreach (var blockChainBlock in blocks)
                foreach (Transaction transaction in blockChainBlock.Transactions)
                {
                    var position = 0;
                    foreach (var transactionOutput in transaction.Outputs)
                        if (transactionOutput.State == OutputStateEnum.UTXO
                          &&
                            transactionOutput.Receiver == sender.Address)
                        {
                            var result = _transactionUtxoRepository.GetInputsByOutputId(transactionOutput.Id);

                            if (result.HasNoValue)
                            {
                                //TODO: checkear que tengo que firmar
                                transactionsOutputsToReturn.Add(new Input(position, transaction.TransactionId,
                                                                          SignatureMessage.Sign(sender.PrivateKey),
                                                                          transactionOutput.Id));
                                totalAmountInOutputs += transactionOutput.Amount;
                                transactionOutput.Spend(sender.PrivateKey);
                                if (totalAmountInOutputs >= (amount + BlockChainConfig.FeePerTransaction))
                                    return transactionsOutputsToReturn;
                            }
                        }
                }

            throw new Exception("Does Not have enough amount");
        }
    }
}