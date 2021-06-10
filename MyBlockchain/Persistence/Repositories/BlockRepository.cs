using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories.Interfaces;

namespace MyBlockChain.Persistence.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly IMongoCollection<BlockDocument> _blocksCollection;
        private readonly IStorageParser _storageParser;

        public BlockRepository(IStorageParser storageParser)
        {
            _storageParser = storageParser;

            _blocksCollection = new MongoClient(
                    "mongodb://localhost:27017"
                ).GetDatabase("blockchain")
                .GetCollection<BlockDocument>("blocks");
        }

        public async Task Insert(Block block)
        {
            await _blocksCollection.InsertOneAsync(new BlockDocument(block));
        }

        public List<Block> GetAll(BlockChain blockChain)
        {
            return _blocksCollection.Find(Builders<BlockDocument>.Filter.Empty)
                .ToList()
                .Select(x => _storageParser.Parse(x, blockChain))
                .ToList();
        }

        public List<Block> GetWithTransactionsToSpent(BlockChain blockChain, Address sender, Amount amount)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(List<Block> blocks)
        {
            //_blocksCollection.InsertMany(blocks);
        }
    }
}