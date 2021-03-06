#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Transactions;

#endregion

namespace MyBlockChain.Persistence.Repositories
{
    public class TransactionsPoolContextFactory : IDesignTimeDbContextFactory<TransactionsPoolContext>
    {
        public TransactionsPoolContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TransactionsPoolContext>();
            optionsBuilder
               .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlockChain-TransactionsPool-Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new TransactionsPoolContext(optionsBuilder.Options);
        }
    }

    public class BlockChainContext : DbContext
    {
        public BlockChainContext(DbContextOptions<BlockChainContext> options)
            : base(options)
        {
        }

        public BlockChainContext()
        {
        }


        public DbSet<WalletDocument> Wallets { get; set; }

        public DbSet<BlockDocument>              Blocks           { get; set; }
        public DbSet<InputDocument>              Inputs           { get; set; }
        public DbSet<OutputDocument>             Outputs          { get; set; }
        public DbSet<TransactionDocument>        Transactions     { get; set; }
        public DbSet<TransactionWithFeeDocument> TransactionsUtxo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

    public class BlockChainContextFactory : IDesignTimeDbContextFactory<BlockChainContext>
    {
        public BlockChainContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlockChainContext>();
            optionsBuilder
               .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlockChain-Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new BlockChainContext(optionsBuilder.Options);
        }
    }
}