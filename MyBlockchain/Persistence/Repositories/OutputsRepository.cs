using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
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

        private readonly IStorageParser _storageParser;
        private readonly BlockChainContext _context;

        public InputsRepository(IStorageParser storageParser,
            BlockChainContext context)
        {
            _storageParser = storageParser;
            _context = context;

        }
        public void Sepend(Input input)
        {
      
        }
    }
    public class OutputsRepository : IOutputsRepository
    {
        private readonly IStorageParser _storageParser;
        private readonly BlockChainContext _context;

        public OutputsRepository(IStorageParser storageParser,
            BlockChainContext context)
        {
            _storageParser = storageParser;
            _context = context;
        }

        public List<Output> GetToSpent(Address receiver, Amount amount)
        {
            var result = _context.Outputs.GroupBy(x => x.Receiver)
                .Select(x => new
                {
                    Receiver = x.Key,
                    Amount = x.Sum(x => x.Amount),
                    List = x.OrderBy(x => x.Amount)
                }).Where(x => x.Amount <= amount)
                .FirstOrDefault().List.Select(x=> _storageParser.Parse(x))
                .ToList();
            return result;
        }

        public List<Output> GetAll(Address receiver)
        {
            return _context.Outputs.Where(x => x.State == OutputStateEnum.UTXO)
                .ToList()
                .Select(_storageParser.Parse)
                .ToList();
        }

        public int GetBalance(Address receiver)
        {
            return _context.Outputs.Where(x => x.State == OutputStateEnum.UTXO
                                               &&
                                               x.Receiver == receiver)
                .Sum(x => x.Amount);
        }

        public void Spent(Output output)
        {
            var result = _context.Outputs.First(x => x.Id == output.Id);
            result.State = OutputStateEnum.Spent;
            _context.SaveChanges();

        }

        //.Find(Builders<BlockDocument>.Filter.AnyIn(y => y.Transactions.an,))
        //.ToList()
        //.Select(x => storageParser.Parse(x, blockChain))
        //.ToList();
    }
}