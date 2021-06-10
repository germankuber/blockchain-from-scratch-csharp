using System.Collections.Generic;
using System.Linq;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

namespace MyBlockChain.Persistence
{
    public class StorageParser : IStorageParser
    {
        private readonly IScriptBlockFactory _scriptBlockFactory;
        private readonly ITransactionIdStrategy _transactionIdStrategy;
        private BlockChain _blockChain;

        public StorageParser(ITransactionIdStrategy transactionIdStrategy,
            IScriptBlockFactory scriptBlockFactory
        )
        {
            _transactionIdStrategy = transactionIdStrategy;
            _scriptBlockFactory = scriptBlockFactory;
        }

        public Block Parse(BlockDocument block, BlockChain blockChain)
        {
            _blockChain = blockChain;
            var transactions = new Blocks.Transactions(block.Transactions.Select(Parse).ToList());

            return Block.CreateMined(Parse(block.Header, transactions),
                transactions);
        }

        public Transaction Parse(TransactionDocument transaction)
        {
            return Transaction.CreateEmpty(
                new TransactionId(transaction.TransactionId),
                Parse(transaction.Inputs),
                Parse(transaction.Outputs),
                _transactionIdStrategy);
        }

        public TransactionDocument Parse(Transaction transaction)
        {
            return new(transaction);
        }


        public Output Parse(OutputDocument output)
        {
            return new(Amount.Create(output.Amount),
                new Address(output.Receiver),
                _scriptBlockFactory);
        }

        private List<Input> Parse(List<InputDocument> inputs)
        {
            return inputs.Select(x => new Input(x.TransactionOutputPosition,
                    new TransactionId(x.TransactionHash),
                    SignatureMessage.Create(x.Signature)))
                .ToList();
        }

        private List<Output> Parse(List<OutputDocument> outputs)
        {
            return outputs.Select(x => new Output(Amount.Create(x.Amount),
                    new Address(x.Receiver),
                    _scriptBlockFactory,
                    x.Id))
                .ToList();
        }

        private BlockHeader Parse(BlockHeaderDocument blockHeader, Blocks.Transactions transactions)
        {
            return new(blockHeader.TimeSpan,
                blockHeader.LastHash,
                blockHeader.Hash,
                blockHeader.Data,
                blockHeader.Nonce,
                blockHeader.Difficulty,
                transactions);
        }
    }
}