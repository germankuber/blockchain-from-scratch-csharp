using MyBlockChain.General;
using MyBlockChain.Transactions;

namespace MyBlockChain.Persistence.Dtos
{
    public class TransactionWithFee
    {
        public TransactionWithFee(Transaction transaction, Amount fee)
        {
            Transaction = transaction;
            Fee = fee;
        }

        public Transaction Transaction { get; }
        public Amount Fee { get; }
    }
}