using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LevelDB;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

using MyBlockChain.Blocks;

namespace MyBlockChain.Persistence
{
    public interface IBlockStorage
    {
        void Insert(Block block);
        void Insert(List<Block> blocks);
    }
    public class BlockStorage : IBlockStorage
    {
        private readonly IMongoCollection<Block> _blocksCollection;

        public BlockStorage()
        {

            _blocksCollection = new MongoClient(
                    "mongodb://localhost:27017"
                ).GetDatabase("blockchain")
                .GetCollection<Block>("blocks");
        }
        public void Insert(Block block)
        {
            _blocksCollection.InsertOne(block);
        }
        public void Insert(List<Block> blocks)
        {
            _blocksCollection.InsertMany(blocks);
        }
    }
}
