using System.Linq;
using FluentAssertions;

using Moq;

using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;
using MyBlockChain.Transactions.MemoryPool;
using Xunit;

namespace MyBlockChain.Tests
{
    public class MinerShould
    {
        private readonly BlockChain _blockChain;
        private readonly Mock<IFeeCalculation> _feeCalculationMock;
        private readonly Wallet _wallet;
        private readonly ITransactionFactory _transactionFactory;
        private readonly PowBlockMineStrategy _powBlockMineStrategy = new();
        private readonly IUnconfirmedTransactionPool _unconfirmedTransactionPool
            = new UnconfirmedTransactionPool(new ValidateTransaction());

        private readonly Miner _miner;

        public MinerShould()
        {
            _blockChain = new BlockChain();
            _feeCalculationMock = new Mock<IFeeCalculation>();

            _transactionFactory = new TransactionFactory(new ValidateTransaction(),
                new CalculateTransactionIdStrategy(),
                new CalculateInputs(_blockChain),
                new CalculateOutputs(_blockChain, new ScriptBlockFactory(), new FeeCalculation()),
                new ScriptBlockFactory());

            _wallet = CreateWallet();
            _miner = new Miner(_blockChain,
                _wallet,
                _unconfirmedTransactionPool,
                _powBlockMineStrategy,
                _transactionFactory);
        }

        
        [Fact]
        public void Make_Transaction_Add_Transaction_To_Memory_Pool()
        {
            var wallet2 = CreateWallet();

            Enumerable.Range(1, 3).Aggregate(Block.Genesis(),
                (_, _) => _blockChain.AddBlock(Block.Mine(_wallet.Address,
                    _blockChain.LastBlock(),
                    "",
                    _powBlockMineStrategy,
                    new Blocks.Transactions(),
                    _transactionFactory)).Value);


            _wallet.MakeTransaction(wallet2.Address, Amount.Create(12));
            _wallet.MakeTransaction(wallet2.Address, Amount.Create(13));
            
            var block = _miner.Mine();
            var transactions = block.Value.Transactions.GetAll().Value;
            transactions.Count.Should().Be(3);
            transactions.First().GetType().Should().Be(typeof(CoinBaseTransaction));


        }
        private Wallet CreateWallet() =>
            new(_blockChain, _feeCalculationMock.Object,
                _transactionFactory,
                _unconfirmedTransactionPool);
    }
}