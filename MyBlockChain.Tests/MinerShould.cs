using System.Threading.Tasks;

using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Core.Operations;
using MyBlockChain.General;
using MyBlockChain.Persistence.Repositories.Interfaces;
using Xunit;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace MyBlockChain.Tests
{
    public class MinerShould : BaseTest
    {

        [Fact]
        public async Task Mine_One_Block_When_There_Are_Not_Transactions()
        {
            await Reset();

            await Miner1.Mine();

            var blocks = ServiceProvider.GetRequiredService<IBlockRepository>()
                .GetAll(BlockChain);
            blocks.Count.Should().Be(1);
        }

        [Fact]
        public async Task Mine_Multiples_Blocks_When_There_Are_Not_Transactions()
        {
            await Reset();
            await Miner1.Mine();
            await Miner1.Mine();
            await Miner1.Mine();
            await Miner1.Mine();

            var blocks = ServiceProvider.GetRequiredService<IBlockRepository>()
                .GetAll(BlockChain);
            blocks.Count.Should().Be(4);
        }

        [Fact]
        public async Task Mine_Gives_Reward_To_The_Miner()
        {
            await Reset();

            await Miner1.Mine();
            await Miner1.Mine();

            var b = Wallet1.GetBalance();
            b.Should().Be(10);
        }

        [Fact]
        public async Task Mine_Transaction_Give_Fee_To_Miner()
        {
            await Reset();
            await Miner1.Mine();
            Wallet1.MakeTransaction(Wallet2.Address, Amount.Create(3));

            await Miner3.Mine();

            Wallet1.GetBalance().Should().Be(Amount.Create(1));
            Wallet2.GetBalance().Should().Be(Amount.Create(3));
            Wallet3.GetBalance().Should().Be(Amount.Create(6));
        }

        //[Fact]
        //public async Task Make_Transaction_Add_Transaction_To_Memory_Pool()
        //{
        //    var wallet2 = CreateWallet();
        //    await _miner.Mine();

        //    var a = _wallet.GetBalance();
        //    var firstTransaction = _wallet.MakeTransaction(wallet2.Address, Amount.Create(1));
        //    var secondTransaction = _wallet.MakeTransaction(wallet2.Address, Amount.Create(2));

        //    var transactions = _serviceProvider.GetRequiredService<IUnconfirmedTransactionPool>()
        //        .GetBestTransactions(2);
        //    transactions.Value.Count.Should().Be(2);
        //    transactions.Value.Any(x => x.TransactionId.Hash == firstTransaction.Value.TransactionId.Hash).Should()
        //        .BeTrue();
        //    transactions.Value.Any(x => x.TransactionId.Hash == secondTransaction.Value.TransactionId.Hash).Should()
        //        .BeTrue();
        //}


    }
}