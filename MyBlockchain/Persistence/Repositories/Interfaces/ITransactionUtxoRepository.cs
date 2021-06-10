﻿using System.Collections.Generic;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Transactions;

namespace MyBlockChain.Persistence.Repositories.Interfaces
{
    public interface ITransactionUtxoRepository
    {
        void Insert(TransactionWithFee transaction);
        List<TransactionWithFee> GetAllUtxo();
        void Delete(TransactionWithFee transaction);
    }
}