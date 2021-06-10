using MyBlockChain.General;
using MyBlockChain.Transactions;

namespace MyBlockChain.Persistence.Dtos
{
    public class TransactionWithFee
    {
        public TransactionWithFee(Transaction transaction, Amount fee, int id)
        {
            Transaction = transaction;
            Fee = fee;
            Id = id;
        }
        public TransactionWithFee(Transaction transaction, Amount fee)
        {
            Transaction = transaction;
            Fee = fee;
        }

        public int Id { get; set; }
        public Transaction Transaction { get; }
        public Amount Fee { get; }
    }
}