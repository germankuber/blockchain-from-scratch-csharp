using MyBlockChain.Blocks;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Persistence
{
    public interface IStorageParser
    {
        Block Parse(BlockDocument block, BlockChain blockChain);
        Output Parse(OutputDocument output);
        Transaction Parse(TransactionDocument transaction);
        TransactionDocument Parse(Transaction transaction);
    }
}