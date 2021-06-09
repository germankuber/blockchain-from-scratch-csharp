using EllipticCurve;

namespace MyBlockChain.General
{
    public static class KeysGenerator
    {
        private static readonly PrivateKey PrivateKey = new();

        public static string GetPublicKey()
        {
            return PrivateKey.publicKey().toString().TransformToString();
        }

        public static string GetPrivateKey() => 
            PrivateKey.toString().TransformToString();

        public static (string PrivateKey, string PublicKey) GetPairKey(string privateKey)
        {
            var pk =  PrivateKey.fromString(privateKey.ToByte());
            return (pk.toString().TransformToString(),
                pk.publicKey().toString().TransformToString());
        }
    }
}