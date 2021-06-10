using System.Collections.Generic;
using System.Linq;
using MyBlockChain.Blocks;
using MyBlockChain.General;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public static class OutputExtensions
    {
        public static Amount GetTotalAmount(this List<Output> @this)
        {
            return Amount.Create(@this.Sum(x => (int) x.Amount));
        }

        public static Amount GetTotalAmount(this List<Input> @this, BlockChain blockChain)
        {
            return @this.Select(input =>
                    blockChain.GetOutputByTransactionIdAndPosition(new TransactionId(input.TransactionHash),
                            input.TransactionOutputPosition)
                        .Value.Amount)
                .ToList()
                .Sum(x => x);
        }
    }
}