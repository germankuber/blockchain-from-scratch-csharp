using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MyBlockChain.General;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Persistence.Repositories
{
 

    public  class InputsRepository:IInputsRepository
    {

        private readonly IMongoCollection<BlockDocument> _blocksCollection;
        private readonly IStorageParser _storageParser;

        public InputsRepository(IStorageParser storageParser)
        {
            _storageParser = storageParser;

            _blocksCollection = new MongoClient(
                    "mongodb://localhost:27017"
                ).GetDatabase("blockchain")
                .GetCollection<BlockDocument>("blocks");
        }
        public void Sepend(Input input)
        {
            var inputr = _blocksCollection.AsQueryable()
                .FirstOrDefault(x => x.Transactions.Any(x => x.TransactionId == input.TransactionHash));


           var toSpent = inputr.Transactions.FirstOrDefault(x => x.TransactionId == input.TransactionHash)
                .Inputs
                .FirstOrDefault(x => x.TransactionHash == input.TransactionHash
                                     &&
                                     x.Signature == input.Signature
                                     &&
                                     x.TransactionOutputPosition == input.TransactionOutputPosition);
        }
    }
    public class OutputsRepository : IOutputsRepository
    {
        private readonly IMongoCollection<BlockDocument> _blocksCollection;
        private readonly IStorageParser _storageParser;

        public OutputsRepository(IStorageParser storageParser)
        {
            _storageParser = storageParser;

            _blocksCollection = new MongoClient(
                    "mongodb://localhost:27017"
                ).GetDatabase("blockchain")
                .GetCollection<BlockDocument>("blocks");
        }

        public List<Output> GetToSpent(Address receiver, Amount amount)
        {
            throw new System.NotImplementedException();
        }

        public List<Output> GetAll(Address receiver)
        {
            return _blocksCollection.AsQueryable()
                .Where(x => x.Transactions.Any(t => t.Outputs.Any(o => o.Receiver == receiver)))
                .Select(s => s.Transactions.Select(o => o.Outputs))
                .SelectMany(w => w.ToList())
                .Where(w => w.Any(s => s.State == OutputStateEnum.UTXO))
                .ToList()
                .Select(x => x.Select(s => _storageParser.Parse(s)))
                .SelectMany(s => s)
                .ToList();
        }

        public void Spent(Output output)
        {
            throw new System.NotImplementedException();
        }

        //.Find(Builders<BlockDocument>.Filter.AnyIn(y => y.Transactions.an,))
        //.ToList()
        //.Select(x => storageParser.Parse(x, blockChain))
        //.ToList();
    }
}