using CSharpFunctionalExtensions;
using MyBlockChain.General;

namespace MyBlockChain.Transactions
{
    public class TransactionId : ValueObject<TransactionId>
    {
        public TransactionId(string hash)
        {
            Hash = new Hash(hash);
        }

        public Hash Hash { get; }
        protected override bool EqualsCore(TransactionId other) => 
            Hash == other.Hash;

        protected override int GetHashCodeCore() => 
            Hash.GetHashCode();

      
    }
}