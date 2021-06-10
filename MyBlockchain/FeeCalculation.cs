using MyBlockChain.Blocks;
using MyBlockChain.General;

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