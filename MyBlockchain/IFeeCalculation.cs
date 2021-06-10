using MyBlockChain.General;

namespace MyBlockChain
{
    public interface IFeeCalculation
    {
        public Amount GetFee();
    }
}