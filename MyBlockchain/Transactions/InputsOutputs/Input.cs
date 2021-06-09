using MyBlockChain.General;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class Input
    {
        public Input(int transactionOutputPosition,
            TransactionId transactionId,
            SignatureMessage signature)
        {
            TransactionOutputPosition = transactionOutputPosition;
            TransactionHash = transactionId.Hash;
            Signature = signature;
        }
        public Input(int transactionOutputPosition,
            Hash hash,
            SignatureMessage signature)
        {
            TransactionOutputPosition = transactionOutputPosition;
            TransactionHash = hash;
            Signature = signature;
        }

        public Hash TransactionHash { get; }
        public int TransactionOutputPosition { get; }
        public SignatureMessage Signature { get; }
    }
}