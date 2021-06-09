using MyBlockChain.Blocks;

namespace MyBlockChain.Persistence
{
    public interface IStorageParser
    {
        Block Parse(BlockDocument block, BlockChain blockChain);
    }
}