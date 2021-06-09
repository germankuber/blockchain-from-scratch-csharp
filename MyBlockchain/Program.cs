﻿using System;
using System.Linq;

using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;
using MyBlockChain.Transactions.MemoryPool;

namespace MyBlockChain
{
    internal class Program
    {
        private static BlockChain _blockChain;
        private static ITransactionFactory _transactionFactory;
        private static readonly PowBlockMineStrategy _powBlockMineStrategy = new();
        private static readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool
            = new UnconfirmedTransactionPool(new ValidateTransaction());

        private static Miner _miner;
        private static Wallet _wallet;

        private static void Main(string[] args)
        {
            _blockChain = new BlockChain(new BlockStorage(null));

            _transactionFactory = new TransactionFactory(new ValidateTransaction(),
                new CalculateTransactionIdStrategy(),
                new CalculateInputs(_blockChain),
                new CalculateOutputs(_blockChain, new ScriptBlockFactory(), new FeeCalculation()),
                new ScriptBlockFactory());
            _wallet = CreateWallet();
            var wallet2 = CreateWallet();

            _miner = new Miner(_blockChain,
                _wallet,
                _unconfirmedTransactionPool,
                _powBlockMineStrategy,
                _transactionFactory);



            //Enumerable.Range(1, 3).Aggregate(Block.Genesis(),
            //    (block, _) => _blockChain.AddBlock(Block.Mine(_wallet.Address,
            //        _blockChain.LastBlock(),
            //        "",
            //        _powBlockMineStrategy,
            //        new Blocks.Transactions(),
            //        _transactionFactory)).Value);


            //var newTransaction = _wallet.MakeTransaction(wallet2.Address, Amount.Create(1));


          var r =  _miner.Mine();

            //var blockChain = new BlockChain();

            //var wallet1 = new Wallet(blockChain, null);
            //var wallet2 = new Wallet(blockChain, null);

            //var signature = wallet1.Sign("hola como andas");


            //var rr = wallet2.Verify("hola como andas",signature, wallet1.Address);


            //Enumerable.Range(1, 20).Aggregate(Block.Genesis(),
            //    (block, i) =>
            //    {
            //        var newBlock = Block.Mine(new PowBlockMineStrategy(block, i.ToString()));
            //        blockChain.AddBlock(newBlock);
            //        return newBlock;
            //    });


            // Generate new Keys
            var bb =_wallet.GetBalance();
            Console.ReadKey();
        }

        private static Wallet CreateWallet() =>
            new(_blockChain, new FeeCalculation(),
                _transactionFactory,
                _unconfirmedTransactionPool);
    }
}