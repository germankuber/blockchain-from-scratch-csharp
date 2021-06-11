#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;

#endregion

namespace MyBlockChain.Persistence.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly BlockChainContext  _context;
        private readonly IOutputsRepository _outputsRepository;
        private readonly IStorageParser     _storageParser;

        public BlockRepository(IStorageParser     storageParser,
                               BlockChainContext  context,
                               IOutputsRepository outputsRepository)
        {
            _storageParser     = storageParser;
            _context           = context;
            _outputsRepository = outputsRepository;
        }

        public async Task Insert(Block block)
        {
            await _context.Blocks.AddAsync(new BlockDocument(block));
        }

        public List<Block> GetAll(BlockChain blockChain)
        {
            return _context.Blocks
                           .Include(x => x.Header)
                           .Include(x => x.Transactions)
                           .ThenInclude(x => x.Inputs)
                           .Include(x => x.Transactions)
                           .ThenInclude(x => x.Outputs)
                           .Select(x => _storageParser.Parse(x, blockChain, _outputsRepository))
                           .ToList();
        }

        public List<Block> GetWithTransactionsToSpent(BlockChain blockChain, Address sender, Amount amount)
        {
            var a = _context.Blocks.Where(x => x.Transactions.Any(x => x.Outputs.Any(o => o.Receiver == sender
                                                                                        &&
                                                                                          o.State == OutputStateEnum
                                                                                             .UTXO)))
                            .Select(x => _storageParser.Parse(x, blockChain, _outputsRepository))
                            .ToList();
            return a;
        }

        public void SaveChange() => _context.SaveChanges();
    }
}