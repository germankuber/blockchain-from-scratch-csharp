#region

using CSharpFunctionalExtensions;

#endregion

namespace MyBlockChain.General
{
    public class Hash : ValueObject<Hash>
    {
        private readonly string _value;

        public Hash(string value)
        {
            _value = value;
        }

        public static implicit operator string(Hash hash)
        {
            return hash._value;
        }

        protected override bool EqualsCore(Hash other)
        {
            return _value == other._value;
        }

        protected override int GetHashCodeCore()
        {
            return _value.GetHashCode();
        }
    }
}