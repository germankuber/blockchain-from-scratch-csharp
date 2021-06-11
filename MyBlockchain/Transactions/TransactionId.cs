#region

using CSharpFunctionalExtensions;
using MyBlockChain.General;

#endregion

namespace MyBlockChain.Transactions
{
    public class TransactionId : ValueObject<TransactionId>
    {
        public TransactionId(string hash)
        {
            Hash = new Hash(hash);
        }

        public Hash Hash { get; }

        protected override bool EqualsCore(TransactionId other)
        {
            return Hash == other.Hash;
        }

        protected override int GetHashCodeCore()
        {
            return Hash.GetHashCode();
        }
    }
}