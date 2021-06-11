#region

using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlockChain.General;
using MyBlockChain.Transactions.InputsOutputs;

#endregion

namespace MyBlockChain.Persistence.Repositories.Interfaces
{
    public interface IOutputsRepository
    {
        List<Output> GetToSpent(Address receiver, Amount amount);
        List<Output> GetAll(Address     receiver);
        void         Spend(int          outputId);
        int          GetBalance(Address receiver);
        Output       GetById(int        id);
        Task         SaveChange();
        Amount       GetBalance(List<Input> receiver);
    }
}