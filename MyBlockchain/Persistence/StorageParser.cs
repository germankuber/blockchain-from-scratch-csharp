using System.Collections.Generic;
using System.Linq;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;
using MyBlockChain.Transactions.MemoryPool;

namespace MyBlockChain.Persistence
{
    public class StorageParser : IStorageParser
    {
        private readonly ITransactionFactory _transactionFactory;
        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool;
        private readonly ITransactionIdStrategy _transactionIdStrategy;
        private readonly IScriptBlockFactory _scriptBlockFactory;
        private BlockChain _blockChain;

        public StorageParser(ITransactionFactory transactionFactory,
            IUnconfirmedTransactionPool unconfirmedTransactionPool,
            ITransactionIdStrategy transactionIdStrategy,
            IScriptBlockFactory scriptBlockFactory)
        {
            _transactionFactory = transactionFactory;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;
            _transactionIdStrategy = transactionIdStrategy;
            _scriptBlockFactory = scriptBlockFactory;
        }

        public Block Parse(BlockDocument block, BlockChain blockChain)
        {
            _blockChain = blockChain;
            var transactions = new Blocks.Transactions(block.Transactions.Select(t =>
                Transaction.CreateEmpty(
                    Parse((List<InputDocument>) t.Inputs),
                    Parse((List<OutputDocument>) t.Outputs),
                    _transactionIdStrategy)).ToList());

            return Block.CreateMined(Parse(block.Header, transactions),
                transactions);
        }

        private List<Input> Parse(List<InputDocument> inputs) =>
            inputs.Select(x => new Input(x.TransactionOutputPosition,
                    new TransactionId(x.TransactionHash),
                    SignatureMessage.Create(x.Signature)))
                .ToList();

        private List<Output> Parse(List<OutputDocument> outputs) =>
            outputs.Select(x => new Output(Amount.Create(x.Amount),
                    new Address(x.Receiver),
                    _scriptBlockFactory))
                .ToList();
        private BlockHeader Parse(BlockHeaderDocument blockHeader, Blocks.Transactions transactions) =>
            new(
                blockHeader.TimeSpan,
                blockHeader.LastHash,
                blockHeader.Hash,
                blockHeader.Data,
                blockHeader.Nonce,
                blockHeader.Difficulty,
                transactions);


        private Wallet CreateWallet() =>
            new(_blockChain, new FeeCalculation(),
                _transactionFactory,
                _unconfirmedTransactionPool);
    }
}