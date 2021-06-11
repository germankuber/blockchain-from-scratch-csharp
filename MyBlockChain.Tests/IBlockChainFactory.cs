#region

using MyBlockChain.Blocks;

#endregion

namespace MyBlockChain.Tests
{
    public interface IBlockChainFactory
    {
        BlockChain Create();
    }
}