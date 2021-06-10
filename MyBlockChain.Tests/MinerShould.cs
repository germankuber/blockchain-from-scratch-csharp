using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Core.Operations;
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
    public class OutputsRepositoryTest : IOutputsRepository
    {
        private readonly IBlockRepository _blockRepository;

        public OutputsRepositoryTest(IBlockRepository blockRepository)
        {
            _blockRepository = blockRepository;
        }

        public List<Output> GetAll(Address receiver)
        {
            return _blockRepository
                .GetAll(null)
                .ToList()
                .Select(x => x.Transactions)
                .Select(x => x.GetAll())
                .Select(x => x.Value.Select(o => o.Outputs))
                .SelectMany(x => x)
                .SelectMany(x => x)
                .Where(x => x.Receiver == receiver && x.State == OutputStateEnum.UTXO)
                .ToList();
        }
        public List<Output> GetToSpent(Address receiver, Amount amount)
        {
            var outputs = _blockRepository
                .GetAll(null)
                .ToList()
                .Select(x => x.Transactions)
                .Select(x => x.GetAll())
                .Select(x => x.Value.Select(o => o.Outputs))
                .SelectMany(x => x)
                .SelectMany(x => x)
                .Where(x => x.Receiver == receiver && x.State == OutputStateEnum.UTXO)
                .ToList();

            var tmpList = new List<Output>();
            var sumAmount = Amount.Create(0);
            foreach (var output in outputs)
            {
                tmpList.Add(output);
                sumAmount += output.Amount;
                if (sumAmount >= amount)
                    return tmpList;
            }

            return tmpList;
        }
        public void Spent(Output output)
        {
            var inputToUpdate = _blockRepository
                .GetAll(null)
                .ToList()
                .Select(x => x.Transactions)
                .Select(x => x.GetAll())
                .Select(x => x.Value.Select(o => o.Outputs))
                .SelectMany(x => x)
                .SelectMany(x => x)
                .FirstOrDefault(x => x.Id == output.Id);
            inputToUpdate.State = OutputStateEnum.Spent;
        }
    }


  

    public interface IBlockChainFactory
    {
        BlockChain Create();
    }

    public class BlockChainFactory : IBlockChainFactory
    {
        private readonly IMediator _mediator;

        public BlockChainFactory(IMediator mediator)
        {
            _mediator = mediator;
        }

        public BlockChain Create()
        {
            return new(_mediator);
        }
    }

    public class BlockRepositoryTest : IBlockRepository
    {
        private readonly List<Block> _list = new();

        public async Task Insert(Block block)
        {
            await Task.Run(() => _list.Add(block));
        }

        public void Insert(List<Block> blocks)
        {
            _list.AddRange(blocks);
        }

        public List<Block> GetAll(BlockChain blockChain)
        {
            return _list;
        }

        public List<Block> GetWithTransactionsToSpent(BlockChain blockChain, Address sender, Amount amount)
        {
            var blocks = _list.Where(x =>
                x.Transactions.GetAll().Value.Any(s =>
                    s.Outputs.Any(o => o.Receiver == sender && o.State == OutputStateEnum.UTXO)))
                .ToList();
            return blocks;
        }
    }

    public abstract class BaseTest
    {
        protected readonly Wallet Wallet1;
        protected readonly Wallet Wallet2;
        protected readonly Wallet Wallet3;
        protected readonly BlockChain BlockChain;
        protected readonly Miner Miner1;
        protected readonly Miner Miner2;
        protected readonly Miner Miner3;
        protected readonly ServiceProvider ServiceProvider;

        protected BaseTest()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
            var blockChainFactory = ServiceProvider.GetRequiredService<IBlockChainFactory>();
            BlockChain = blockChainFactory.Create();
            Wallet1 = CreateWallet();
            Wallet2 = CreateWallet();
            Wallet3 = CreateWallet();
            Miner1 = CreateMiner(Wallet1);
            Miner2 = CreateMiner(Wallet2);
            Miner3 = CreateMiner(Wallet3);
        }

        private Miner CreateMiner(Wallet wallet)
        {
            return new Miner(BlockChain,
                wallet,
                ServiceProvider.GetRequiredService<IUnconfirmedTransactionPool>(),
                ServiceProvider.GetRequiredService<IBlockMineStrategy>(),
                ServiceProvider.GetRequiredService<ITransactionFactory>());
        }

        private Wallet CreateWallet()
        {
            return new(BlockChain, ServiceProvider.GetRequiredService<IFeeCalculation>(),
                ServiceProvider.GetRequiredService<ITransactionFactory>(),
                ServiceProvider.GetRequiredService<IUnconfirmedTransactionPool>(),
                ServiceProvider.GetRequiredService<IOutputsRepository>());
        }
        protected static void ConfigureServices(ServiceCollection services)
        {
            services
                .AddScoped<IFeeCalculation, FeeCalculation>()
                .AddScoped<ITransactionFactory, TransactionFactory>()
                .AddScoped<IBlockMineStrategy, PowBlockMineStrategy>()
                .AddScoped<IUnconfirmedTransactionPool, UnconfirmedTransactionPool>()
                .AddScoped<IValidateTransaction, ValidateTransaction>()
                .AddScoped<ITransactionIdStrategy, CalculateTransactionIdStrategy>()
                .AddScoped<IScriptBlockFactory, ScriptBlockFactory>()
                .AddScoped<IStorageParser, StorageParser>()
                .AddScoped<ICalculateInputs, CalculateInputs>()
                .AddScoped<ICalculateOutputs, CalculateOutputs>()
                .AddScoped<IWalletStorage, WalletStorage>()
                .AddScoped<IOutputsRepository, OutputsRepositoryTest>()
                .AddScoped<IBlockRepository, BlockRepositoryTest>()
                .AddScoped<ITransactionStorage, TransactionStorageTest>()
                .AddScoped<IBlockChainFactory, BlockChainFactory>()
                .AddScoped<IInputsRepository, InputsRepository>()
                //TODO: in memory

                .AddMediatR(typeof(Transaction));
        }
    }

    public class MinerShould : BaseTest
    {

        [Fact]
        public async Task Mine_One_Block_When_There_Are_Not_Transactions()
        {
            await Miner1.Mine();

            var blocks = ServiceProvider.GetRequiredService<IBlockRepository>()
                .GetAll(BlockChain);
            blocks.Count.Should().Be(1);
        }

        [Fact]
        public async Task Mine_Multiples_Blocks_When_There_Are_Not_Transactions()
        {
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
            await Miner1.Mine();
            await Miner1.Mine();

            Wallet1.GetBalance().Should().Be(10);
        }

        [Fact]
        public async Task Mine_Transaction_Give_Fee_To_Miner()
        {
            await Miner1.Mine();
            Wallet1.GetBalance().Should().Be(5);
            Wallet1.MakeTransaction(Wallet2.Address, Amount.Create(3));

            await Miner3.Mine();

            Wallet1.GetBalance().Should().Be(1);
            Wallet2.GetBalance().Should().Be(3);
            Wallet3.GetBalance().Should().Be(6);
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