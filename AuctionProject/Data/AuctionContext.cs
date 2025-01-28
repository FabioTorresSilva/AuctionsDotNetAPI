using AuctionProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionProject.Data
{
    public class AuctionContext : DbContext
    {
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
        { }

        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Auction> Auctions { get; set; } = null!;

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>()
                .HasMany(i => i.Categories)
                .WithMany(c => c.Items)
                .UsingEntity(j => j.ToTable("ItemCategories"));
        }
    }
}
