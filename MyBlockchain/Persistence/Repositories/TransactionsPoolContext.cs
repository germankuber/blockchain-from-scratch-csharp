using Microsoft.EntityFrameworkCore;
using MyBlockChain.Persistence.Dtos;
using MyBlockChain.Transactions;

namespace MyBlockChain.Persistence.Repositories
{
    public class TransactionsPoolContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public TransactionsPoolContext(DbContextOptions<TransactionsPoolContext> options)
            : base(options)
        { }
        public TransactionsPoolContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionWithFeeDocument>()
                .HasOne(b => b.Transaction)
                .WithOne(i => i.TransactionWithFeeDocument)
                .HasForeignKey<TransactionDocument>(b => b.TransactionWithFeeDocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<TransactionWithFeeDocument> TransactionsUtxo { get; set; }
    }
}