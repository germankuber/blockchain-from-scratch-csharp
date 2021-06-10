using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;

namespace MyBlockChain.Persistence.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly IStorageParser _storageParser;
        private readonly BlockChainContext _context;

        public BlockRepository(IStorageParser storageParser,
            BlockChainContext context)
        {
            _storageParser = storageParser;
            _context = context;

        }

        public async Task Insert(Block block)
        {
            await _context.Blocks.AddAsync(new BlockDocument(block));
            await _context.SaveChangesAsync();
        }

        public List<Block> GetAll(BlockChain blockChain)
        {
            return _context.Blocks
                .Include(x=> x.Header)
                .Include( x=> x.Transactions)
                .ThenInclude(x=> x.Inputs)
                .Include(x => x.Transactions)
                .ThenInclude(x => x.Outputs)
                .Select(x => _storageParser.Parse(x, blockChain))
                .ToList();
        }

        public List<Block> GetWithTransactionsToSpent(BlockChain blockChain, Address sender, Amount amount)
        {
            return _context.Blocks.Where(x => x.Transactions.Any(x => x.Outputs.Any(o => o.Receiver == sender
                &&
                o.State == OutputStateEnum.UTXO)))
                .Select(x=>_storageParser.Parse(x, blockChain))
                .ToList();
        }

        public void SaveChange() => _context.SaveChanges();
    }
}