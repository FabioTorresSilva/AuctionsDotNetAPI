using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionProject.Services.Stats.Interfaces
{
    /// <summary>
    /// Interface for category-related statistics in the auction system.
    /// </summary>
    public interface ICategoryStats
    {
        /// <summary>
        /// Gets the total number of categories.
        /// </summary>
        /// <returns>Total number of categories.</returns>
        Task<int> GetTotalCategoriesAsync();

        /// <summary>
        /// Gets the number of auctions for a specific category.
        /// </summary>
        Task<int> GetAuctionsByCategoryAsync(int categoryId);

        /// <summary>
        /// Gets the most popular category based on the number of auctions.
        /// </summary>
        Task<string> GetMostPopularCategoryAsync();

        /// <summary>
        /// Gets the distribution of auctions across all categories.
        /// </summary>
        Task<Dictionary<int, int>> GetAuctionDistributionByCategoryAsync();
    }
}
