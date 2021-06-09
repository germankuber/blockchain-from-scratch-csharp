using CSharpFunctionalExtensions;

namespace MyBlockChain.Transactions
{
    public interface IValidateTransaction
    {
        public Result<Transaction> Validate(Transaction transaction);
    }
}