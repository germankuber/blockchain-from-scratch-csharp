using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using MyBlockChain.Persistence.Events;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Blocks
{
    public class BlockChain
    {
        private readonly List<Block> _blocks = new();
        private readonly IMediator _mediator;

        public BlockChain(IMediator mediator)
        {
            _mediator = mediator;
            _blocks.Add(Block.Genesis());
        }

        public IReadOnlyCollection<Block> Blocks => _blocks.AsReadOnly();

        public Block LastBlock()
        {
            return Blocks.Last();
        }

        public async Task<Result<Block>> AddBlock(Block block)
        {
            return await Result.SuccessIf(block.Header.Hash.Substring(0, block.Header.Difficulty)
                                          == string.Concat(Enumerable.Repeat("0", block.Header.Difficulty)),
                    true, "The hash in the block does not match with the actual difficulty")
                .OnSuccessTry(async _ => await CreateBlock(block));
        }

        private async Task<Block> CreateBlock(Block block)
        {
            _blocks.Add(block);
            await _mediator.Send(new SaveBlockInStorageHandler.SaveBlockInStorageCommand(block));
            return block;
        }


        public Maybe<Transaction> GetTransactionById(TransactionId id)
        {
            foreach (var block in Blocks)
            {
                var transaction = block.Transactions.GetTransactionById(id);
                if (transaction.HasValue)
                    return transaction;
            }

            return Maybe<Transaction>.None;
        }

        public Maybe<Output> GetOutputByTransactionIdAndPosition(TransactionId id, int position)
        {
            foreach (var block in Blocks)
            {
                var transaction = block.Transactions.GetTransactionById(id);
                if (transaction.HasValue)
                    return transaction.Value.Outputs[position];
            }

            return Maybe<Output>.None;
        }
    }
}