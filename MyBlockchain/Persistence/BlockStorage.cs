using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Driver;

using MyBlockChain.Blocks;

namespace MyBlockChain.Persistence
{
    public static class InputsRepository
    {
        private static IMongoCollection<BlockDocument> _blocksCollection = new MongoClient(
            "mongodb://localhost:27017"
        ).GetDatabase("blockchain")
            .GetCollection<BlockDocument>("blocks");
        public static List<Block> GetAll(BlockChain blockChain, IStorageParser storageParser) =>
            _blocksCollection.Find(Builders<BlockDocument>.Filter.Eq(x=> x.Transactions.Any(),true))
                .ToList()
                .Select(x => storageParser.Parse(x, blockChain))
                .ToList();


    }
    public class BlockStorage : IBlockStorage
    {
        private readonly IStorageParser _storageParser;
        private readonly IMongoCollection<BlockDocument> _blocksCollection;

        public BlockStorage(IStorageParser storageParser)
        {
            _storageParser = storageParser;

            _blocksCollection = new MongoClient(
                    "mongodb://localhost:27017"
                ).GetDatabase("blockchain")
                .GetCollection<BlockDocument>("blocks");
        }
        public void Insert(Block block)
        {
            _blocksCollection.InsertOne(new BlockDocument(block));
        }
        public List<Block> GetAll(BlockChain blockChain) =>
            _blocksCollection.Find(Builders<BlockDocument>.Filter.Empty)
                .ToList()
                .Select(x=>_storageParser.Parse(x, blockChain))
                .ToList();

        public void Insert(List<Block> blocks)
        {
            //_blocksCollection.InsertMany(blocks);
        }
    }
}