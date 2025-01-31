using AuctionProject.Data;
using AuctionProject.Models;
using AuctionProject.Models.Enums;
using AuctionProject.Services.Stats.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionProject.Services.Stats
{
    /// <summary>
    /// Service for retrieving category-related statistics in the auction system.
    /// </summary>
    public class CategoryStatsService : ICategoryStats
    {
        private readonly AuctionContext _context;

        public CategoryStatsService(AuctionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the total number of categories.
        /// </summary>
        /// <returns>Total number of categories.</returns>
        public async Task<int> GetTotalCategoriesAsync()
        {
            // Count the total number of categories
            return await _context.Categories.CountAsync();
        }

        /// <summary>
        /// Gets the number of auctions for a specific category.
        /// </summary>
        /// <param name="categoryId">The ID of the category.</param>
        /// <returns>The number of auctions associated with the category.</returns>
        public async Task<int> GetAuctionsByCategoryAsync(int categoryId)
        {
            // Count the number of auctions where the item belongs to the given category
            return await _context.Auctions
                .Where(a => a.Item.Categories.Any(c => c.Id == categoryId))
                .CountAsync();
        }

        /// <summary>
        /// Gets the most popular category based on the number of auctions.
        /// </summary>
        /// <returns>The name of the most popular category.</returns>
        public async Task<string> GetMostPopularCategoryAsync()
        {
            var mostPopularCategory = await _context.Auctions
                .Where(a => a.Item.Categories.Any())
                .SelectMany(a => a.Item.Categories)
                .GroupBy(c => c.Id)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            if (mostPopularCategory == 0)
            {
                return "No categories available";
            }

            var categoryName = await _context.Categories
                .Where(c => c.Id == mostPopularCategory)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();

            return categoryName ?? "Unknown Category";
        }

        /// <summary>
        /// Gets the distribution of auctions across all categories.
        /// </summary>
        /// <returns>A dictionary with category IDs as keys and auction counts as values.</returns>
        public async Task<Dictionary<int, int>> GetAuctionDistributionByCategoryAsync()
        {
            var categoryDistribution = await _context.Auctions
                .Where(a => a.Item.Categories.Any())
                .SelectMany(a => a.Item.Categories)
                .GroupBy(c => c.Id)
                .Select(g => new { CategoryId = g.Key, AuctionCount = g.Count() })
                .ToListAsync();

            return categoryDistribution.ToDictionary(x => x.CategoryId, x => x.AuctionCount);
        }
    }
}
