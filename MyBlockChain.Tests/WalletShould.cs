using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Moq;

using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;

using Xunit;
using static System.String;

namespace MyBlockChain.Tests
{
    public class WalletShould
    {
        private readonly BlockChain _blockChain;
        private readonly Mock<IFeeCalculation> _feeCalculationMock;
        private readonly Wallet _sut;
        private readonly ITransactionFactory _transactionFactory;

        public WalletShould()
        {
            _blockChain = new BlockChain();
            _feeCalculationMock = new Mock<IFeeCalculation>();

            _transactionFactory = new TransactionFactory(new ValidateTransaction(),
                new CalculateTransactionIdStrategy(),
                new CalculateInputs(_blockChain),
                new CalculateOutputs(_blockChain, new ScriptBlockFactory()),
                new ScriptBlockFactory());

            _sut = new Wallet(_blockChain, _feeCalculationMock.Object,
                _transactionFactory,
                Amount.Create(1000));
        }

        [Fact]
        public void Has_The_Same_Address()
        {
            string address = _sut.Address;
            address.Should().Be(KeysGenerator.GetPublicKey());
        }

        //[Fact]
        //public void Return_Current_Balance_After_5_Coinbase()
        //{
        //   Enumerable.Range(1, 3).Aggregate(Block.Genesis(),
        //        (_, _) => _blockChain.AddBlock(Block.Mine(_sut.Address,
        //            new PowBlockMineStrategy(_blockChain.Blocks.Last(), Empty),
        //            new Blocks.Transactions(),
        //            _transactionFactory)).Value);
            
        //   _sut.GetBalance().Should().Be(45);

        //}

        [Theory]
        [InlineData(3,12)]
        [InlineData(7, 28)]
        public void Make_Transaction(int spendAmount, int finalBalance)
        {
            var wallet2 = CreateWallet();


            Enumerable.Range(1, 3).Aggregate(Block.Genesis(),
                (_, _) => _blockChain.AddBlock(Block.Mine(_sut.Address,
                    new PowBlockMineStrategy(_blockChain.Blocks.Last(), Empty),
                    new Blocks.Transactions(),
                    _transactionFactory)).Value);


            Enumerable.Range(1, 4).Aggregate(Block.Genesis(),
                (_, _) => _blockChain.AddBlock(Block.Mine(_sut.Address,
                    new PowBlockMineStrategy(_blockChain.Blocks.Last(), Empty),
                    new Blocks.Transactions(_sut.MakeTransaction(wallet2.Address, Amount.Create(spendAmount)).Value),
                    _transactionFactory)).Value);
            int balance = wallet2.GetBalance();
            balance.Should().Be(finalBalance);

        }

        private Wallet CreateWallet() =>
            new(_blockChain, _feeCalculationMock.Object,
                _transactionFactory);
    }
}