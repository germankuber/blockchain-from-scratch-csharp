using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MyBlockChain.Blocks;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.MemoryPool;

namespace MyBlockChain.Persistence.Repositories
{
    public class WalletStorage : IWalletStorage
    {
        private readonly IMongoCollection<WalletDocument> _blocksCollection;
        private readonly IFeeCalculation _feeCalculation;
        private readonly IOutputsRepository _outputsRepository;
        private readonly ITransactionFactory _transactionFactory;
        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool;

        public WalletStorage(IFeeCalculation feeCalculation,
            ITransactionFactory transactionFactory,
            IUnconfirmedTransactionPool unconfirmedTransactionPool,
            IOutputsRepository outputsRepository)
        {
            _feeCalculation = feeCalculation;
            _transactionFactory = transactionFactory;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;
            _outputsRepository = outputsRepository;

            _blocksCollection = new MongoClient(
                    "mongodb://localhost:27017"
                ).GetDatabase("blockchain")
                .GetCollection<WalletDocument>("wallets");
        }

        public void Insert(Wallet wallet)
        {
            _blocksCollection.InsertOne(new WalletDocument(wallet));
        }

        public List<Wallet> GetAll(BlockChain blockChain)
        {
            return _blocksCollection.Find(Builders<WalletDocument>.Filter.Empty)
                .ToList()
                .Select(x => new Wallet(blockChain,
                    _feeCalculation,
                    _transactionFactory,
                    _unconfirmedTransactionPool,
                    _outputsRepository,
                    x.PrivateKey))
                .ToList();
        }
    }
}