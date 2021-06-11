#region

using MyBlockChain.General;
using MyBlockChain.Transactions;

#endregion

namespace MyBlockChain.Persistence.Dtos
{
    public class TransactionWithFee
    {
        public TransactionWithFee(TransactionDocument transaction, Amount fee, int id)
        {
            Transaction = transaction;
            Fee         = fee;
            Id          = id;
        }

        public TransactionWithFee(TransactionDocument transaction, Amount fee)
        {
            Transaction = transaction;
            Fee         = fee;
        }

        public int                 Id          { get; set; }
        public TransactionDocument Transaction { get; }
        public Amount              Fee         { get; }
    }
}