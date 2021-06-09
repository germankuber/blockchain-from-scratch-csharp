using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class Output
    {
        private readonly IScriptBlock _scriptBlock;
        public OutputStateEnum State { get; private set; } = OutputStateEnum.UTXO;

        public Output(Amount value,
            Address receiver,
            IScriptBlockFactory scriptBlockFactory)
        {
            Amount = value;
            Receiver = receiver;
            _scriptBlock = scriptBlockFactory.Create(receiver);
        }

        public bool Spend(string privateKey)
        {
            //TODO: Verify the way to mark as spent
            State = OutputStateEnum.Spent;
            return _scriptBlock.Excecute(privateKey);
        }

        public Amount Amount { get; }
        public Address Receiver { get; }
    }
}