using System;
using System.Configuration;
using System.Windows.Forms;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using MyBlockChain.Persistence;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Persistence.Repositories;
using MyBlockChain.Persistence.Repositories.Interfaces;
using MyBlockChain.Transactions;
using MyBlockChain.Transactions.InputsOutputs;
using MyBlockChain.Transactions.InputsOutputs.Scripts;
using MyBlockChain.Transactions.MemoryPool;

namespace MyBlockChain.UI
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            ConfigureServices(services);

            using var serviceProvider = services.BuildServiceProvider();
            var wallet = serviceProvider.GetRequiredService<Wallet>();
            Application.Run(wallet);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services
                .AddScoped<Wallet>()
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
                .AddScoped<IInputsRepository, InputsRepository>()
                .AddScoped<IBlockRepository, BlockRepository>()
                .AddScoped<ITransactionUtxoRepository, TransactionUtxoUtxoRepository>()
                .AddDbContext<BlockChainContext>(options =>
                    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlockChain;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                .AddDbContext<TransactionsPoolContext>(options =>
                    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlockChain-TransactionsPool;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                .AddMediatR(typeof(Transaction));
        }
    }
}