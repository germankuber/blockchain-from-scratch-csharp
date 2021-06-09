namespace MyBlockChain.Transactions
{
    public class TransactionId
    {
        public TransactionId(string hash)
        {
            Hash = hash;
        }

        public string Hash { get; }
    }
}