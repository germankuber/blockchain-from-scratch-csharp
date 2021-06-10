using System.Collections.Generic;
using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Persistence.Repositories.Interfaces
{
    public interface IOutputsRepository
    {
        List<Output> GetToSpent(Address receiver, Amount amount);
        List<Output> GetAll(Address receiver);
        void Spent(Output output);
    }
}