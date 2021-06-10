using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MyBlockChain.General;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;

namespace MyBlockChain.Persistence.Dtos
{
    public class TransactionStorage : ITransactionStorage
    {
        private readonly IMongoCollection<TransactionWithFeeDocument> _blocksCollection;
        private readonly IStorageParser _storageParser;

        public TransactionStorage(IStorageParser storageParser)
        {
            _storageParser = storageParser;

            _blocksCollection = new MongoClient(
                    "mongodb://localhost:27017"
                ).GetDatabase("blockchain")
                .GetCollection<TransactionWithFeeDocument>("transactions-utxo");
        }


        public void Insert(TransactionWithFee transaction)
        {
            _blocksCollection.InsertOne(new TransactionWithFeeDocument(
                _storageParser.Parse(transaction.Transaction), transaction.Fee));
        }

        public void Delete(Transaction transaction)
        {
            var toDelete = _blocksCollection.AsQueryable()
                .FirstOrDefault(x => x.Transaction.TransactionId == transaction.TransactionId.Hash);
            _blocksCollection.DeleteOne(Builders<TransactionWithFeeDocument>.Filter.Eq(x => x.Id, toDelete.Id));
        }

        public List<TransactionWithFee> GetAllUtxo()
        {
            return _blocksCollection.Find(Builders<TransactionWithFeeDocument>.Filter.Empty)
                .ToList()
                .Select(x => new TransactionWithFee(_storageParser.Parse(x.Transaction), Amount.Create(x.TotalFee)))
                .ToList();
        }
    }
}