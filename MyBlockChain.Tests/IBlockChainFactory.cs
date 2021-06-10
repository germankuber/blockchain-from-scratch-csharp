using MyBlockChain.Blocks;

namespace MyBlockChain.Tests
{
    public interface IBlockChainFactory
    {
        BlockChain Create();
    }
}