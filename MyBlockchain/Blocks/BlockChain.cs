using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace MyBlockChain.Blocks
{
    public class BlockChain
    {
        private readonly List<Block> _blocks = new();

        public BlockChain()
        {
            _blocks.Add(Block.Genesis());
        }

        public IReadOnlyCollection<Block> Blocks => _blocks.AsReadOnly();

        public Result AddBlock(Block block)
        {
            return Result.SuccessIf(block.Header.Hash.Substring(0, block.Header.Difficulty)
                                    == string.Concat(Enumerable.Repeat("0", block.Header.Difficulty)),
                    true, "The hash in the block does not match with the actual difficulty")
                .OnSuccessTry(_ => _blocks.Add(block));
        }
    }
}