#region

using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBlockChain.Blocks;
using MyBlockChain.Persistence;
using MyBlockChain.Persistence.Repositories;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;
using MyBlockChain.Transactions.MemoryPool;
using Respawn;

#endregion

namespace MyBlockChain.Tests
{
    public abstract class BaseTest
    {
        protected readonly BlockChain      BlockChain;
        protected readonly Miner           Miner1;
        protected readonly Miner           Miner2;
        protected readonly Miner           Miner3;
        protected readonly ServiceProvider ServiceProvider;
        protected readonly Wallet          Wallet1;
        protected readonly Wallet          Wallet2;
        protected readonly Wallet          Wallet3;

        private readonly Checkpoint _checkpoint = new Checkpoint
                                                  {
                                                      SchemasToExclude = new[]
                                                                         {
                                                                             "__EFMigrationsHistory"
                                                                         }
                                                  };

        private readonly Checkpoint _checkpoint2 = new Checkpoint
                                                   {
                                                       SchemasToExclude = new[]
                                                                          {
                                                                              "__EFMigrationsHistory"
                                                                          }
                                                   };

        protected string ConnectionStringBlockChain =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlockChain-Tests;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected string ConnectionStringPool =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlockChain-TransactionsPool-Tests;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected BaseTest()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
            var blockChainFactory = ServiceProvider.GetRequiredService<IBlockChainFactory>();
            BlockChain = blockChainFactory.Create();
            Wallet1    = CreateWallet("00AD0764F77047F532C3DC1718B111C824319F615B0204E604A70FC367CEE88A");
            Wallet2    = CreateWallet("7220EB7DE6AC7DE5F67C1BA154099EB060A3CAED0D57F73351D1E9A2115AC946");
            Wallet3    = CreateWallet("62EACB9FF7A4BBBD336A5940FC5D68ABF2D27DE0AD76555B8BE6848E0957E1A5");
            Miner1     = CreateMiner(Wallet1);
            Miner2     = CreateMiner(Wallet2);
            Miner3     = CreateMiner(Wallet3);
            var blockChainContext       = ServiceProvider.GetRequiredService<BlockChainContext>();
            var transactionsPoolContext = ServiceProvider.GetRequiredService<TransactionsPoolContext>();
            blockChainContext.Database.EnsureCreated();
            transactionsPoolContext.Database.EnsureCreated();
            try
            {
                blockChainContext.Database.Migrate();
                transactionsPoolContext.Database.Migrate();
            }
            catch (Exception)
            {
            }
        }

        protected async Task Reset()
        {
            await _checkpoint.Reset(ConnectionStringBlockChain);
            await _checkpoint2.Reset(ConnectionStringPool);
        }

        private Miner CreateMiner(Wallet wallet) =>
            new(BlockChain,
                wallet,
                ServiceProvider.GetRequiredService<IUnconfirmedTransactionPool>(),
                ServiceProvider.GetRequiredService<IBlockMineStrategy>(),
                ServiceProvider.GetRequiredService<ITransactionFactory>());

        private Wallet CreateWallet(string privateKey)
        {
            return new(BlockChain, ServiceProvider.GetRequiredService<IFeeCalculation>(),
                       ServiceProvider.GetRequiredService<ITransactionFactory>(),
                       ServiceProvider.GetRequiredService<IUnconfirmedTransactionPool>(),
                       ServiceProvider.GetRequiredService<IOutputsRepository>(),
                       privateKey);
        }

        protected void ConfigureServices(ServiceCollection services)
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
               .AddScoped<IOutputsRepository, OutputsRepository>()
               .AddScoped<IBlockRepository, BlockRepository>()
               .AddScoped<ITransactionUtxoRepository, TransactionUtxoUtxoRepository>()
               .AddScoped<IBlockChainFactory, BlockChainFactory>()
               .AddScoped<IInputsRepository, InputsRepository>()
               .AddDbContext<BlockChainContext>(options =>
                                                    options.UseSqlServer(ConnectionStringBlockChain))
               .AddDbContext<TransactionsPoolContext>(options =>
                                                          options.UseSqlServer(ConnectionStringPool))
               .AddMediatR(typeof(Transaction));
        }
    }
}