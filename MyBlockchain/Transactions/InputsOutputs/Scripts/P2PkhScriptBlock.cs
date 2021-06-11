#region

using EllipticCurve;
using MyBlockChain.General;

#endregion

namespace MyBlockChain.Transactions.InputsOutputs.Scripts
{
    public class P2PkhScriptBlock : IScriptBlock
    {
        private readonly Address _receiver;

        public P2PkhScriptBlock(Address receiver)
        {
            _receiver = receiver;
        }

        public bool Excecute(string privateKey)
        {
            var key = PrivateKey.fromString(privateKey.ToByte());
            return key.publicKey().ToString() == _receiver;
        }
    }
}