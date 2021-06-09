using CSharpFunctionalExtensions;
using MyBlockChain.Transactions;

namespace MyBlockChain.General
{
    public class Hash : ValueObject<Hash>
    {
        private readonly string _value;

        public Hash(string value)
        {
            _value = value;
        }

        public static implicit operator string(Hash hash) =>
            hash._value;

        protected override bool EqualsCore(Hash other) =>
            _value == other._value;

        protected override int GetHashCodeCore() =>
            _value.GetHashCode();
    }
}