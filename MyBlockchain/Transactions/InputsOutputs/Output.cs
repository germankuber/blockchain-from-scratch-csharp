using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class Output
    {
        private readonly IScriptBlock _scriptBlock;
        public int  Id { get; set; }
        public Output(Amount value,
            Address receiver,
            IScriptBlockFactory scriptBlockFactory)
        {
            Amount = value;
            Receiver = receiver;
            _scriptBlock = scriptBlockFactory.Create(receiver);
        }

        public OutputStateEnum State { get;  set; } = OutputStateEnum.UTXO;

        public Amount Amount { get; }
        public Address Receiver { get; }

        public bool Spend(string privateKey)
        {
            //TODO: Verify the way to mark as spent
            State = OutputStateEnum.Spent;
            return _scriptBlock.Excecute(privateKey);
        }
    }
}