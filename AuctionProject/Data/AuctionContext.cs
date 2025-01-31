using AuctionProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionProject.Data
{
    /// <summary>
    /// Represents the database context for the Auction project.
    /// </summary>
    public class AuctionContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionContext"/> class.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
        { }

        /// <summary>
        /// Gets or sets the collection of items in the database.
        /// </summary>
        public DbSet<Item> Items { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of auctions in the database.
        /// </summary>
        public DbSet<Auction> Auctions { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of categories in the database.
        /// </summary>
        public DbSet<Category> Categories { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of managers in the database.
        /// </summary>
        public DbSet<Manager> Managers { get; set; } = null!;

        /// <summary>
        /// Configures the model relationships and constraints for the context.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and delete behaviors for Auction entity
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.Item)
                .WithMany()
                .HasForeignKey(a => a.ItemId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Auction>()
                .HasOne(a => a.Manager)
                .WithMany()
                .HasForeignKey(a => a.ManagerId)
                .OnDelete(DeleteBehavior.NoAction); 

            // Configure many-to-many relationship between Item and Category entities
            modelBuilder.Entity<Item>()
                .HasMany(i => i.Categories)
                .WithMany(c => c.Items)
                .UsingEntity(j => j.ToTable("ItemCategories"));
        }
    }
}
