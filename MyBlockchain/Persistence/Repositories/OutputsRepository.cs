#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;

#endregion

namespace MyBlockChain.Persistence.Repositories
{
    public class InputsRepository : IInputsRepository
    {
        private readonly BlockChainContext _context;

        private readonly IStorageParser _storageParser;

        public InputsRepository(IStorageParser    storageParser,
                                BlockChainContext context)
        {
            _storageParser = storageParser;
            _context       = context;
        }

        public void Sepend(Input input)
        {
        }
    }

    public class OutputsRepository : IOutputsRepository
    {
        private readonly BlockChainContext _context;
        private readonly IStorageParser    _storageParser;

        public OutputsRepository(IStorageParser    storageParser,
                                 BlockChainContext context)
        {
            _storageParser = storageParser;
            _context       = context;
        }

        public List<Output> GetToSpent(Address receiver, Amount amount)
        {
            var result = _context.Outputs.GroupBy(x => x.Receiver)
                                 .Select(x => new
                                              {
                                                  Receiver = x.Key,
                                                  Amount   = x.Sum(x => x.Amount),
                                                  List     = x.OrderBy(x => x.Amount)
                                              }).Where(x => x.Amount <= amount)
                                 .FirstOrDefault().List.Select(x => _storageParser.Parse(x))
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


        public Output GetById(int id) =>
            _context.Outputs.Where(x => x.Id == id).AsEnumerable()
                    .Select(_storageParser.Parse)
                    .FirstOrDefault();

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        public Amount GetBalance(List<Input> receiver)
        {
            //TODO: Parche
            if (receiver.Any(x => x.OutputId == 0))
                return BlockChainConfig.AmountPerMine;
            return _context.Outputs.Where(x => receiver.Select(i => i.OutputId).Any(a => a == x.Id))
                           .Sum(x => x.Amount);
        }

        public int GetBalance(Address receiver)
        {
            var result = _context.Outputs.Where(x => x.State == OutputStateEnum.UTXO
                                                   &&
                                                     x.Receiver == receiver).ToList();
            return result.Sum(x => x.Amount);
        }

        public void Spend(int outputId)
        {
            var result                       = _context.Outputs.FirstOrDefault(x => x.Id == outputId);
            if (result != null) result.State = OutputStateEnum.Spent;
        }

        //.Find(Builders<BlockDocument>.Filter.AnyIn(y => y.Transactions.an,))
        //.ToList()
        //.Select(x => storageParser.Parse(x, blockChain))
        //.ToList();
    }
}