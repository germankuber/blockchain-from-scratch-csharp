#region

using CSharpFunctionalExtensions;
using SimpleBase;

#endregion

namespace MyBlockChain.General
{
    public class Address : ValueObject<Address>
    {
        private readonly string _publicKey;
        private readonly string _hash;

        public Address(string publicKey)
        {
            _publicKey = publicKey;
            _hash      = Base58.Bitcoin.Encode(_publicKey.ToByte());
        }

        public static implicit operator string(Address b)
        {
            return b._publicKey;
        }

        public static implicit operator byte[](Address b)
        {
            return b.ToByte();
        }

        protected override bool EqualsCore(Address other)
        {
            return _publicKey == other._publicKey;
        }

        protected override int GetHashCodeCore()
        {
            return _publicKey.GetHashCode()
                 + _hash.GetHashCode();
        }
    }
}