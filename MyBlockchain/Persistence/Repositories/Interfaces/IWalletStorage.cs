using System.Collections.Generic;
using MyBlockChain.Blocks;

namespace MyBlockChain.Persistence.Repositories.Interfaces
{
    public interface IWalletStorage
    {
        void Insert(Wallet wallet);
        List<Wallet> GetAll(BlockChain blockChain);
    }
}