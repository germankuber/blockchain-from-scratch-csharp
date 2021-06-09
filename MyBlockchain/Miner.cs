using CSharpFunctionalExtensions;
using MyBlockChain.Blocks;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.MemoryPool;

namespace MyBlockChain
{
    public class Miner
    {
        private readonly BlockChain _blockChain;
        private readonly Wallet _miner;
        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool;
        private readonly IBlockMineStrategy _blockMineStrategy;
        private readonly ITransactionFactory _trsTransactionFactory;

        public Miner(BlockChain blockChain,
            Wallet miner,
            IUnconfirmedTransactionPool unconfirmedTransactionPool,
            IBlockMineStrategy blockMineStrategy,
            ITransactionFactory trsTransactionFactory)
        {
            _blockChain = blockChain;
            _miner = miner;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;
            _blockMineStrategy = blockMineStrategy;
            _trsTransactionFactory = trsTransactionFactory;
        }

        public Result<Block> Mine() =>
            _unconfirmedTransactionPool.GetBestTransactions(BlockChainConfig.MaxTransactionsPerBlock)
                .ToResult("There is not transactions")
                .Bind(t => _blockChain.AddBlock(Block.Mine(_miner.Address,
                    _blockChain.LastBlock(),
                    "",
                    _blockMineStrategy,
                    new Blocks.Transactions(t),
                    _trsTransactionFactory)))
                .OnFailureCompensate(_ => _blockChain.AddBlock(Block.Mine(_miner.Address,
                    _blockChain.LastBlock(),
                    "",
                    _blockMineStrategy,
                    new Blocks.Transactions(),
                    _trsTransactionFactory)));
    }
}