#region

using MyBlockChain.Transactions.InputsOutputs;

#endregion

namespace MyBlockChain.Transactions
{
    public class InputDocument
    {
        public InputDocument(Input input)
        {
            TransactionOutputPosition = input.TransactionOutputPosition;
            TransactionHash           = input.TransactionHash;
            Signature                 = input.Signature;
            OutputId                  = input.OutputId;
        }

        public InputDocument()
        {
        }

        public int                 Id                  { get; set; }
        public int                 OutputId            { get; set; }
        public TransactionDocument TransactionDocument { get; set; }
        public string              TransactionHash     { get; set; }

        public int TransactionOutputPosition { get; set; }

        public string Signature { get; set; }
    }
}