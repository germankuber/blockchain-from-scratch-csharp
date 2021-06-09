using System.Collections.Generic;
using MyBlockChain.Blocks;

namespace MyBlockChain.Persistence
{
    public interface IBlockStorage
    {
        void Insert(Block block);
        void Insert(List<Block> blocks);
    }
}