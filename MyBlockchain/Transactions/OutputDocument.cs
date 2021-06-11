#region

using MyBlockChain.Transactions.InputsOutputs;

#endregion

namespace MyBlockChain.Transactions
{
    public class OutputDocument
    {
        public OutputDocument(Output output)
        {
            Amount   = output.Amount;
            Receiver = output.Receiver;
            State    = output.State;
        }

        public OutputDocument()
        {
        }

        public int             Id    { get; set; }
        public OutputStateEnum State { get; set; } = OutputStateEnum.UTXO;

        public int Amount { get; set; }

        public string Receiver { get; set; }
    }
}