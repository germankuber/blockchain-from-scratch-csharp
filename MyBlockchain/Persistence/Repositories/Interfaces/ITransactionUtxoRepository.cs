#region

using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MyBlockChain.General;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Transactions.InputsOutputs;

#endregion

namespace MyBlockChain.Persistence.Repositories.Interfaces
{
    public interface ITransactionUtxoRepository
    {
        void                     Insert(TransactionWithFee transaction);
        List<TransactionWithFee> GetAllUtxo();
        void                     Delete(TransactionWithFee       transaction);
        List<Input>              GetInputsByOutputsIds(List<int> outputsIds);
        Maybe<Input>             GetInputsByOutputId(int         outputsId);
        List<Output>             GetOutputsToSpend(Address       address);
    }
}