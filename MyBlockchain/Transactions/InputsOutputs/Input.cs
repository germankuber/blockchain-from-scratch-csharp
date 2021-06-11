#region

using MyBlockChain.General;

#endregion

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class Input
    {
        public Input(int              transactionOutputPosition,
                     TransactionId    transactionId,
                     SignatureMessage signature,
                     int              outputId)
        {
            TransactionOutputPosition = transactionOutputPosition;
            TransactionHash           = transactionId.Hash;
            Signature                 = signature;
            OutputId                  = outputId;
        }

        public Input(int              transactionOutputPosition,
                     Hash             hash,
                     SignatureMessage signature,
                     int              outputId)
        {
            TransactionOutputPosition = transactionOutputPosition;
            TransactionHash           = hash;
            Signature                 = signature;
            OutputId                  = outputId;
        }

        public int              OutputId                  { get; set; }
        public Hash             TransactionHash           { get; }
        public int              TransactionOutputPosition { get; }
        public SignatureMessage Signature                 { get; }
    }
}