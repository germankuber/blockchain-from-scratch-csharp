using EllipticCurve;

namespace MyBlockChain.General
{
    public static class KeysGenerator
    {
        private static readonly PrivateKey _privateKey = new();

        public static string GetPublicKey()
        {
            return _privateKey.publicKey().toString().TransformToString();
        }

        public static string GetPrivateKey()
        {
            return _privateKey.toString().TransformToString();
        }
    }
}