namespace MyBlockChain.Transactions
{
    public interface ITransactionIdStrategy
    {
        TransactionId Calculate(Transaction transaction);
    }
}