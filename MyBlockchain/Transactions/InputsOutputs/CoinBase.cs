using MyBlockChain.General;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class CoinBase : Input
    {
        public CoinBase(string signature)
            : base(0, new Hash(), signature)
        {
        }
    }
}