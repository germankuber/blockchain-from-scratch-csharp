#region

using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

#endregion

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class Output
    {
        private readonly IScriptBlock _scriptBlock;

        public Output(Amount              value,
                      Address             receiver,
                      IScriptBlockFactory scriptBlockFactory)
        {
            Amount       = value;
            Receiver     = receiver;
            _scriptBlock = scriptBlockFactory.Create(receiver);
        }

        public Output(Amount              value,
                      Address             receiver,
                      IScriptBlockFactory scriptBlockFactory,
                      int                 id)
        {
            Amount       = value;
            Receiver     = receiver;
            _scriptBlock = scriptBlockFactory.Create(receiver);
            Id           = id;
        }

        public int Id { get; }

        public OutputStateEnum State { get; set; } = OutputStateEnum.UTXO;

        public Amount  Amount   { get; }
        public Address Receiver { get; }

        public bool Spend(string privateKey)
        {
            //TODO: Verify the way to mark as spent
            State = OutputStateEnum.Spent;
            return _scriptBlock.Excecute(privateKey);
        }
    }
}