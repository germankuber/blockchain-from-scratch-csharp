namespace MyBlockChain.Persistence.Dtos
{
    public class WalletDocument
    {
        public WalletDocument(Wallet wallet)
        {
            Address    = wallet.Address;
            PrivateKey = wallet.PrivateKey;
        }

        public WalletDocument()
        {
        }

        public int Id { get; set; }

        public string Address { get; set; }

        public string PrivateKey { get; set; }
    }
}