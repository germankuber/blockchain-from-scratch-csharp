using System.Linq;
using System.Text.Json;
using MyBlockChain.General;

namespace MyBlockChain.Transactions
{
    public class CalculateTransactionIdStrategy : ITransactionIdStrategy
    {
        public TransactionId Calculate(Transaction transaction)
        {
            return new(HashUtilities.Hash(JsonSerializer.Serialize(new
            {
                Inputs = string.Join('-', transaction.Inputs.Select(x => (string) x.TransactionHash)),
                Outputs = string.Join('-', transaction.Outputs.Select(x => (string) x.Receiver))
            })));
        }
    }
}