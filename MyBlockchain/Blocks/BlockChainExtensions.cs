using System;
using System.Collections.Generic;
using MyBlockChain.General;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;

namespace MyBlockChain.Blocks
{
    public static class BlockChainExtensions
    {
        public static List<TOut> MapInputs<TOut>(this BlockChain blockChain,
            Address address,
            Func<Output, int, TOut> map)
        {
            var inputs = new List<TOut>();
            foreach (var blockChainBlock in blockChain.Blocks)
            {
                foreach (Transaction transaction in blockChainBlock.Transactions)
                {
                    var position = 0;
                    foreach (var transactionOutput in transaction.Outputs) inputs.Add(map(transactionOutput, position));
                }
            }

            return inputs;
        }
        public static Amount GetBalance(this BlockChain blockChain,
            Address address)
        {
            var balance = Amount.Create(0);
            foreach (var blockChainBlock in blockChain.Blocks)
            {
                foreach (Transaction transaction in blockChainBlock.Transactions)
                {
                    foreach (var transactionOutput in transaction.Outputs)
                        if (transactionOutput.Receiver == address)
                            balance += transactionOutput.Amount;

                }
            }

            return balance;
        }
    }
}