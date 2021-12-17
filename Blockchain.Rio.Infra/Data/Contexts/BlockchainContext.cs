using Blockchain.Rio.Domain.AggregateModel.BlockAggregate;
using Blockchain.Rio.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Rio.Infra.Data.Contexts
{
    public class BlockchainContext : DbContext
    {
        public BlockchainContext(DbContextOptions<BlockchainContext> options) : base(options) { }

        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Block> Block { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionMapping());
            modelBuilder.ApplyConfiguration(new BlockMapping());
        }
    }
}
