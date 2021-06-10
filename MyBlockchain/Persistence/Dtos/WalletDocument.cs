using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBlockChain.Persistence.Dtos
{
    public class WalletDocument
    {
        public WalletDocument(Wallet wallet)
        {
            Address = wallet.Address;
            PrivateKey = wallet.PrivateKey;
        }

        [BsonElement("_id")] public ObjectId Id { get; set; }

        [BsonElement("address")] public string Address { get; set; }

        [BsonElement("privateKey")] public string PrivateKey { get; set; }
    }
}