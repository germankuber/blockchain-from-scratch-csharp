#region

using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MyBlockChain.General;
using MyBlockChain.Persistence.Repositories.Interfaces;
using Xunit;

#endregion

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

        [Theory]
        [InlineData(3, 1, 3, 6)]
        [InlineData(5, 5, 0, 5)]
        public async Task Mine_Transaction_Give_Fee_To_Miner(int sendAmount, int balance1, int balance2, int balance3)
        {
            await Reset();
            await Miner1.Mine();

            Wallet1.MakeTransaction(Wallet2.Address, Amount.Create(sendAmount));

            await Miner3.Mine();
            Wallet1.GetBalance().Should().Be(Amount.Create(balance1));

            Wallet2.GetBalance().Should().Be(Amount.Create(balance2));
            Wallet3.GetBalance().Should().Be(Amount.Create(balance3));
        }
    }
}