using Blockchain.Rio.Domain.AggregateModel.BlockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blockchain.Rio.Infra.Data.Mappings
{
    public class BlockMapping : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> builder)
        {
            builder.ToTable("Block");

            builder.HasKey(it => it.Id);
            builder.Property(it => it.TimeStamp).IsRequired();
            builder.Property(it => it.Hash).IsRequired();
            builder.Property(it => it.PrevHash).IsRequired();
            builder.Property(it => it.Nounce).IsRequired();

            builder.HasMany(s => s.TransactionList);
        }
    }
}
