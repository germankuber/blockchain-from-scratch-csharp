#region

using MyBlockChain.Blocks;
using MyBlockChain.General;

#endregion

namespace MyBlockChain
{
    public class FeeCalculation : IFeeCalculation
    {
        public Amount GetFee()
        {
            return BlockChainConfig.FeePerTransaction;
        }
    }
}