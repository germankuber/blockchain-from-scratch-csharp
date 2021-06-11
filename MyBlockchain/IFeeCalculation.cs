#region

using MyBlockChain.General;

#endregion

namespace MyBlockChain
{
    public interface IFeeCalculation
    {
        public Amount GetFee();
    }
}