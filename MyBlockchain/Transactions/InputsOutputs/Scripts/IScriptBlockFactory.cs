using MyBlockChain.General;

namespace MyBlockChain.Transactions.InputsOutputs.Scripts
{
    public interface IScriptBlockFactory
    {
        IScriptBlock Create(Address receiver);
    }
}