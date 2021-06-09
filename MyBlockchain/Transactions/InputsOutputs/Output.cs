using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class Output
    {
        private IScriptBlock _scriptBlock;

        public Output(Amount value,
            Address receiver,
            IScriptBlockFactory scriptBlockFactory)
        {
            Amount = value;
            Receiver = receiver;
            _scriptBlock = scriptBlockFactory.Create(receiver);
        }

        public Amount Amount { get; }
        public Address Receiver { get; }
    }
}