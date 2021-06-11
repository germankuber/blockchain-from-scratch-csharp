#region

using Microsoft.EntityFrameworkCore;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Transactions;

#endregion

namespace MyBlockChain.Persistence.Repositories
{
    public class TransactionsPoolContext : DbContext
    {
        public TransactionsPoolContext(DbContextOptions<TransactionsPoolContext> options)
            : base(options)
        {
        }

        public TransactionsPoolContext()
        {
        }

        public DbSet<TransactionWithFeeDocument> TransactionsUtxo { get; set; }
        public DbSet<InputDocument>              Inputs           { get; set; }
        public DbSet<OutputDocument>             Outputs          { get; set; }
        public DbSet<TransactionDocument>        Transactions     { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}