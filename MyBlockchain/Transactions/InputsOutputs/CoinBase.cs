#region

using MyBlockChain.General;

#endregion

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class CoinBase : Input
    {
        public CoinBase(string signature)
            : base(0,
                   new Hash("COIN_BASE_HASH"),
                   SignatureMessage.Sign(signature),
                   -1)
        {
        }
    }
}