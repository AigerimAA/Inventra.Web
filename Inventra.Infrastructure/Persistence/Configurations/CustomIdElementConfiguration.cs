using Inventra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventra.Infrastructure.Persistence.Configurations
{
    public class CustomIdElementConfiguration : IEntityTypeConfiguration<CustomIdElement>
    {
        public void Configure(EntityTypeBuilder<CustomIdElement> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FormatString).HasMaxLength(200);
            builder.Property(e => e.FixedValue).HasMaxLength(200);

            builder.HasOne(e => e.Format)
                .WithMany(f => f.Elements)
                .HasForeignKey(e => e.FormatId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
