#region

using MyBlockChain.Transactions.InputsOutputs;

#endregion

namespace MyBlockChain.Persistence.Repositories.Interfaces
{
    public interface IInputsRepository
    {
        void Sepend(Input input);
    }
}