using SimpleBase;

namespace MyBlockChain.General
{
    public class Address
    {
        private readonly string _publicKey;
        private string _hash;

        public Address(string publicKey)
        {
            _publicKey = publicKey;
            _hash = Base58.Bitcoin.Encode(_publicKey.ToByte());
        }

        public static implicit operator string(Address b)
        {
            return b._publicKey;
        }

        public static implicit operator byte[](Address b)
        {
            return b.ToByte();
        }
    }
}