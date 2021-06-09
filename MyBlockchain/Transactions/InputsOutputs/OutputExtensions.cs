using System.Collections.Generic;
using System.Linq;
using MyBlockChain.General;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public static class OutputExtensions
    {
        public static Amount GetTotalAmount(this List<Output> @this)
        {
            return Amount.Create(@this.Sum(x => (int) x.Amount));
        }
    }
}