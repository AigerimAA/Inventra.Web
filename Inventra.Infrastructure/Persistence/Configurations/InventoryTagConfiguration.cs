using Inventra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventra.Infrastructure.Persistence.Configurations
{
    public class InventoryTagConfiguration : IEntityTypeConfiguration<InventoryTag>
    {
        public void Configure(EntityTypeBuilder<InventoryTag> builder)
        {
            builder.HasKey(it => new { it.InventoryId, it.TagId });

            builder.HasOne(it => it.Inventory)
                .WithMany(inv => inv.InventoryTags)
                .HasForeignKey(it => it.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(it => it.Tag)
                .WithMany(t => t.InventoryTags)
                .HasForeignKey(it => it.TagId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
