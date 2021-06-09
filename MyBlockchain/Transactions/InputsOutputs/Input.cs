using MyBlockChain.General;

namespace MyBlockChain.Transactions.InputsOutputs
{
    public class Input
    {
        public Input(int transactionOutputPosition,
            Hash transactionHash,
            string signature)
        {
            TransactionOutputPosition = transactionOutputPosition;
            TransactionHash = transactionHash;
            Signature = signature;
        }

        public Hash TransactionHash { get; }
        public int TransactionOutputPosition { get; }
        public string Signature { get; }
    }
}