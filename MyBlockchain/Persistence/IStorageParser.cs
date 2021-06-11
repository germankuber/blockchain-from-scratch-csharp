#region

using MyBlockChain.Blocks;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;

#endregion

namespace MyBlockChain.Persistence
{
    public interface IStorageParser
    {
        public Block        Parse(BlockDocument block, BlockChain blockChain, IOutputsRepository outputsRepository);
        Output              Parse(OutputDocument output);
        Transaction         Parse(TransactionDocument transaction, IOutputsRepository outputsRepository);
        TransactionDocument Parse(Transaction transaction);
        Input               Parse(InputDocument input);
    }
}