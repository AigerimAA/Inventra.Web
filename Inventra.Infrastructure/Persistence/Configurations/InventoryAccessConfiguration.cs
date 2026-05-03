using Inventra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventra.Infrastructure.Persistence.Configurations
{
    public class InventoryAccessConfiguration : IEntityTypeConfiguration<InventoryAccess>
    {
        public void Configure(EntityTypeBuilder<InventoryAccess> builder)
        {
            builder.HasKey(ia => new { ia.InventoryId, ia.UserId });

            builder.HasOne(ia => ia.Inventory)
                .WithMany(inv => inv.AccessList)
                .HasForeignKey(ia => ia.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ia => ia.User)
                .WithMany()
                .HasForeignKey(ia => ia.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
