using Blockchain.Rio.Domain.AggregateModel.BlockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blockchain.Rio.Infra.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(it => it.Id);
            builder.Property(it => it.From).HasColumnName("From").IsRequired().HasMaxLength(100);
            builder.Property(it => it.To).HasColumnName("To").IsRequired().HasMaxLength(100);
            builder.Property(it => it.Value).HasColumnName("Value").IsRequired().HasMaxLength(500);
        }
    }
}
