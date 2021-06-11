#region

using System.Collections.Generic;
using System.Linq;
using MyBlockChain.Blocks;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.MemoryPool;

#endregion

namespace MyBlockChain.Persistence.Repositories
{
    public class WalletStorage : IWalletStorage
    {
        private readonly BlockChainContext           _context;
        private readonly IFeeCalculation             _feeCalculation;
        private readonly IOutputsRepository          _outputsRepository;
        private readonly ITransactionFactory         _transactionFactory;
        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool;

        public WalletStorage(IFeeCalculation             feeCalculation,
                             ITransactionFactory         transactionFactory,
                             IUnconfirmedTransactionPool unconfirmedTransactionPool,
                             IOutputsRepository          outputsRepository,
                             BlockChainContext           context)
        {
            _feeCalculation             = feeCalculation;
            _transactionFactory         = transactionFactory;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;
            _outputsRepository          = outputsRepository;
            _context                    = context;
        }

        public void Insert(Wallet wallet)
        {
            _context.Wallets.Add(new WalletDocument(wallet));
            _context.SaveChanges();
        }

        public List<Wallet> GetAll(BlockChain blockChain) =>
            _context.Wallets
                    .Select(x => new Wallet(blockChain,
                                            _feeCalculation,
                                            _transactionFactory,
                                            _unconfirmedTransactionPool,
                                            _outputsRepository,
                                            x.PrivateKey))
                    .ToList();
    }
}