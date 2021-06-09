using System.Collections.Generic;
using System.Linq;

using MongoDB.Driver;

using MyBlockChain.Blocks;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.MemoryPool;

namespace MyBlockChain.Persistence
{
    public interface IWalletStorage
    {
        void Insert(Wallet wallet);
        List<Wallet> GetAll(BlockChain blockChain);
    }

    public class WalletStorage : IWalletStorage
    {
        private readonly IFeeCalculation _feeCalculation;
        private readonly ITransactionFactory _transactionFactory;
        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool;
        private readonly IMongoCollection<WalletDocument> _blocksCollection;

        public WalletStorage(IFeeCalculation feeCalculation,
            ITransactionFactory transactionFactory,
            IUnconfirmedTransactionPool unconfirmedTransactionPool)
        {
            _feeCalculation = feeCalculation;
            _transactionFactory = transactionFactory;
            _unconfirmedTransactionPool = unconfirmedTransactionPool;

            _blocksCollection = new MongoClient(
                    "mongodb://localhost:27017"
                ).GetDatabase("blockchain")
                .GetCollection<WalletDocument>("wallets");
        }
        public void Insert(Wallet wallet)
        {
            _blocksCollection.InsertOne(new WalletDocument(wallet));
        }
        public List<Wallet> GetAll(BlockChain blockChain) =>
            _blocksCollection.Find(Builders<WalletDocument>.Filter.Empty)
                .ToList()
                .Select(x => new Wallet(blockChain,
                        _feeCalculation,
                        _transactionFactory,
                        _unconfirmedTransactionPool,
                        x.PrivateKey))
                .ToList();
    }
}