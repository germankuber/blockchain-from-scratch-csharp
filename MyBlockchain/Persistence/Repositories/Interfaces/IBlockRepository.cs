#region

using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlockChain.Blocks;
using MyBlockChain.General;

#endregion

namespace MyBlockChain.Persistence.Repositories.Interfaces
{
    public interface IBlockRepository
    {
        Task        Insert(Block                          block);
        List<Block> GetAll(BlockChain                     blockChain);
        List<Block> GetWithTransactionsToSpent(BlockChain blockChain, Address sender, Amount amount);
        void        SaveChange();
    }
}