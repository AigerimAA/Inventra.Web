using Inventra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventra.Infrastructure.Persistence.Configurations
{
    public class InventorySequenceConfiguration : IEntityTypeConfiguration<InventorySequence>
    {
        public void Configure(EntityTypeBuilder<InventorySequence> builder)
        {
            builder.HasKey(s => s.InventoryId);

            builder.HasOne(s => s.Inventory)
                .WithOne(inv => inv.Sequence)
                .HasForeignKey<InventorySequence>(s => s.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
