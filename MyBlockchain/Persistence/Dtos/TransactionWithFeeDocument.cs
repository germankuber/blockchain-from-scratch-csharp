#region

using MyBlockChain.Transactions;

#endregion

namespace MyBlockChain.Persistence.Dtos
{
    public class TransactionWithFeeDocument
    {
        public TransactionWithFeeDocument(TransactionDocument transaction, int totalFee)
        {
            Transaction = transaction;
            TotalFee    = totalFee;
        }

        public TransactionWithFeeDocument()
        {
        }

        public int                 Id          { get; set; }
        public int                 TotalFee    { get; set; }
        public bool                Spend       { get; set; } = false;
        public TransactionDocument Transaction { get; set; }
    }
}