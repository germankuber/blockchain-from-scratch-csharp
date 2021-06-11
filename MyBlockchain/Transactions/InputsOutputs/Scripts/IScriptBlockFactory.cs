#region

using MyBlockChain.General;

#endregion

namespace MyBlockChain.Transactions.InputsOutputs.Scripts
{
    public interface IScriptBlockFactory
    {
        IScriptBlock Create(Address receiver);
    }
}