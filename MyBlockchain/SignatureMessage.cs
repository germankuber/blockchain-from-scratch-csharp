#region

using EllipticCurve;
using MyBlockChain.General;

#endregion

namespace MyBlockChain
{
    public class SignatureMessage
    {
        private readonly string _value;

        private SignatureMessage(string value)
        {
            _value = value;
        }

        public static SignatureMessage Sign(string message)
        {
            return new(Ecdsa.sign(message,
                                  PrivateKey.fromString(KeysGenerator.GetPrivateKey().ToByte())).toBase64());
        }

        public static SignatureMessage Create(string signedMessage)
        {
            return new(signedMessage);
        }

        public override string ToString()
        {
            return _value;
        }

        public static implicit operator string(SignatureMessage signature)
        {
            return signature._value;
        }
    }
}