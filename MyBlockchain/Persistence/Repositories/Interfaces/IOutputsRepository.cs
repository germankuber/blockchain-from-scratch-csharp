using System.Collections.Generic;
using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Persistence.Repositories.Interfaces
{
    public interface IInputsRepository
    {
        void Sepend(Input input);
    }
}