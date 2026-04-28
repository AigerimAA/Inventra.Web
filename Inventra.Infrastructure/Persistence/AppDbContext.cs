using Inventra.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<InventoryTag> InventoryTags => Set<InventoryTag>();
        public DbSet<InventoryAccess> InventoryAccesses => Set<InventoryAccess>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Like> Likes => Set<Like>();
        public DbSet<CustomIdFormat> CustomIdFormats => Set<CustomIdFormat>();
        public DbSet<CustomIdElement> CustomIdElements => Set<CustomIdElement>();
        public DbSet<InventorySequence> InventorySequence => Set<InventorySequence>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<InventoryTag>()
                .HasKey(it => new { it.InventoryId, it.TagId });

            builder.Entity<InventoryAccess>()
                .HasKey(ia => new { ia.InventoryId, ia.UserId });

            builder.Entity<Like>()
                .HasKey(l => new { l.ItemId, l.UserId });

            builder.Entity<InventorySequence>()
                .HasKey(s => s.InventoryId);

            builder.Entity<Item>()
                .HasIndex(i => new { i.InventoryId, i.CustomId })
                .IsUnique();

            builder.Entity<Item>()
                .HasOne(i => i.Inventory)
                .WithMany(inv => inv.Items)
                .HasForeignKey(i => i.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.Inventory)
                .WithMany(inv => inv.Comments)
                .HasForeignKey(c => c.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Like>()
                .HasOne(l => l.Item)
                .WithMany(i => i.Likes)
                .HasForeignKey(l => l.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CustomIdFormat>()
                .HasOne(f => f.Inventory)
                .WithOne(inv => inv.CustomIdFormat)
                .HasForeignKey<CustomIdFormat>(f => f.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CustomIdElement>()
                .HasOne(e => e.Format)
                .WithMany(f => f.Elements)
                .HasForeignKey(e => e.FormatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InventorySequence>()
                .HasOne(s => s.Inventory)
                .WithOne(inv => inv.Sequence)
                .HasForeignKey<InventorySequence>(s => s.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InventoryTag>()
                .HasOne(it => it.Inventory)
                .WithMany(inv => inv.InventoryTags)
                .HasForeignKey(it => it.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InventoryAccess>()
                .HasOne(ia => ia.Inventory)
                .WithMany(inv => inv.AccessList)
                .HasForeignKey(ia => ia.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
