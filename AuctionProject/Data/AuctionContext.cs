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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

                      // Seed data for default categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Furniture" },
                new Category { Id = 3, Name = "Antiques" },
                new Category { Id = 4, Name = "Art" },
                new Category { Id = 5, Name = "Collectibles" },
                new Category { Id = 6, Name = "Vehicles" },
                new Category { Id = 7, Name = "RealEstate" },
                new Category { Id = 8, Name = "Jewelry" },
                new Category { Id = 9, Name = "Fashion" },
                new Category { Id = 10, Name = "Books" },
                new Category { Id = 11, Name = "Other" }
            );
        }
    }
}
