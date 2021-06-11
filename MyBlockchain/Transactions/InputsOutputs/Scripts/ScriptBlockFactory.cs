#region

using MyBlockChain.General;

#endregion

namespace MyBlockChain.Transactions.InputsOutputs.Scripts
{
    public class ScriptBlockFactory : IScriptBlockFactory
    {
        public IScriptBlock Create(Address receiver)
        {
            return new P2PkhScriptBlock(receiver);
        }
    }
}