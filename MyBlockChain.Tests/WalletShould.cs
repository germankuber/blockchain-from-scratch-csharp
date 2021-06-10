using FluentAssertions;
using Moq;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Persistence;
using MyBlockChain.Persistence.Repositories;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;
using MyBlockChain.Transactions.MemoryPool;
using Xunit;

namespace MyBlockChain.Tests
{
    public class WalletShould
    {
        private readonly BlockChain _blockChain;

        private readonly Mock<IBlockRepository> _blockStorageMock = new();
        private readonly Mock<IFeeCalculation> _feeCalculationMock;
        private readonly PowBlockMineStrategy _powBlockMineStrategy = new();
        private readonly Wallet _sut;
        private readonly ITransactionFactory _transactionFactory;

        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool;

        public WalletShould()
        {
            _blockStorageMock.Setup(x => x.Insert(It.IsAny<Block>()));
            _blockChain = new BlockChain(null);
            _feeCalculationMock = new Mock<IFeeCalculation>();

            _transactionFactory = new TransactionFactory(new ValidateTransaction(),
                new CalculateTransactionIdStrategy(),
                new CalculateInputs(new OutputsRepository(new StorageParser(new CalculateTransactionIdStrategy(), new ScriptBlockFactory())),new BlockRepository(new StorageParser(new CalculateTransactionIdStrategy(), new ScriptBlockFactory()))),
                new CalculateOutputs(new ScriptBlockFactory(), new FeeCalculation()),
                new ScriptBlockFactory());

            //_sut = CreateWallet();
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

        //[Theory]
        //[InlineData(3,12)]
        //[InlineData(7, 28)]
        //public void Make_Transaction_Successfully(int spendAmount, int finalBalance)
        //{
        //    var wallet2 = CreateWallet();


        //    Enumerable.Range(1, 3).Aggregate(Block.Genesis(),
        //        (_, _) => _blockChain.AddBlock(Block.Mine(_sut.Address,
        //            new PowBlockMineStrategy(_blockChain.Blocks.Last(), Empty),
        //            new Blocks.Transactions(),
        //            _transactionFactory)).Value);


        //    Enumerable.Range(1, 4).Aggregate(Block.Genesis(),
        //        (_, _) => _blockChain.AddBlock(Block.Mine(_sut.Address,
        //            new PowBlockMineStrategy(_blockChain.Blocks.Last(), Empty),
        //            new Blocks.Transactions(_sut.MakeTransaction(wallet2.Address, Amount.Create(spendAmount)).Value),
        //            _transactionFactory)).Value);
        //    int balance = wallet2.GetBalance();

        //    balance.Should().Be(finalBalance);
        //    _blockChain.Blocks.Count.Should().Be(8);
        //}
        //[Fact]
        //public void Make_Transaction_Add_Transaction_To_Memory_Pool()
        //{
        //    var wallet2 = CreateWallet();

        //    Enumerable.Range(1, 3).Aggregate(Block.Genesis(),
        //        (_, _) => _blockChain.AddBlock(Block.Mine(_sut.Address,
        //            _blockChain.LastBlock(),
        //            "",
        //            _powBlockMineStrategy,
        //            new Blocks.Transactions(),
        //            _transactionFactory)).Result.Value);


        //    var newTransaction = _sut.MakeTransaction(wallet2.Address, Amount.Create(12));

        //    _unconfirmedTransactionPool.TotalTransactions().Should().Be(1);
        //    _unconfirmedTransactionPool.GetBestTransactions(1)
        //        .Value
        //        .FirstOrDefault()
        //        .TransactionId
        //        .Should()
        //        .Be(newTransaction.Value.TransactionId);
        //}

        //private Wallet CreateWallet()
        //{
        //    return new(_blockChain, _feeCalculationMock.Object,
        //        _transactionFactory,
        //        _unconfirmedTransactionPool);
        //}
    }
}