using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlockChain.Blocks;
using MyBlockChain.General;

namespace MyBlockChain.Persistence.Repositories.Interfaces
{
    public interface IBlockRepository
    {
        Task Insert(Block block);
        void Insert(List<Block> blocks);
        List<Block> GetAll(BlockChain blockChain);
        List<Block> GetWithTransactionsToSpent(BlockChain blockChain,  Address sender, Amount amount);
    }
}