using EllipticCurve;
using MyBlockChain.General;

namespace MyBlockChain
{
    public class SignatureMessage
    {
        private readonly string _value;

        private SignatureMessage(string value)
        {
            _value = value;
        }
        public static SignatureMessage Sign(string message) =>
            new(Ecdsa.sign(message,
                PrivateKey.fromString(KeysGenerator.GetPrivateKey().ToByte())).toBase64());
        public override string ToString() => _value;
    }
}