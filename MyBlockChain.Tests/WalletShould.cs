using FluentAssertions;
using Moq;
using MyBlockChain.Blocks;
using MyBlockChain.General;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;
using Xunit;

namespace MyBlockChain.Tests
{
    public class WalletShould
    {
        private readonly BlockChain _blockChain;
        private readonly Mock<IFeeCalculation> _feeCalculationMock;
        private readonly Wallet _sut;

        public WalletShould()
        {
            _blockChain = new BlockChain();
            _feeCalculationMock = new Mock<IFeeCalculation>();
            _sut = new Wallet(_blockChain, _feeCalculationMock.Object,
                new TransactionFactory(new ValidateTransaction(),
                    new CalculateTransactionIdStrategy(),
                    new CalculateInputs(_blockChain),
                    new CalculateOutputs(_blockChain),
                    new ScriptBlockFactory())
            );
        }

        [Fact]
        public void Has_The_Same_Address()
        {
            string address = _sut.Address;
            address.Should().Be(KeysGenerator.GetPublicKey());
        }
    }
}