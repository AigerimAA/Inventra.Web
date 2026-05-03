using Inventra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventra.Infrastructure.Persistence.Configurations
{
    public class CustomIdConfiguration : IEntityTypeConfiguration<CustomIdFormat>
    {
        public void Configure(EntityTypeBuilder<CustomIdFormat> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasOne(f => f.Inventory)
                .WithOne(inv => inv.CustomIdFormat)
                .HasForeignKey<CustomIdFormat>(f => f.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
