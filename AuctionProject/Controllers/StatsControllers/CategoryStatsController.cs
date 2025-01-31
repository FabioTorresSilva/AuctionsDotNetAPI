using AuctionProject.Services.Stats.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionProject.Controllers
{
    /// <summary>
    /// Controller for handling category-related statistics in the auction system.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryStatsController : ControllerBase
    {
        private readonly ICategoryStats _categoryStatsService;

        public CategoryStatsController(ICategoryStats categoryStatsService)
        {
            _categoryStatsService = categoryStatsService;
        }

        /// <summary>
        /// Gets the total number of categories in the auction system.
        /// </summary>
        /// <returns>The total number of categories.</returns>
        [HttpGet("total-categories")]
        public async Task<ActionResult<int>> GetTotalCategories()
        {
            var totalCategories = await _categoryStatsService.GetTotalCategoriesAsync();
            return Ok(totalCategories);
        }

        /// <summary>
        /// Gets the number of auctions associated with a specific category.
        /// </summary>
        /// <param name="categoryId">The ID of the category to retrieve statistics for.</param>
        /// <returns>The number of auctions in the specified category.</returns>
        [HttpGet("auctions/{categoryId}")]
        public async Task<ActionResult<int>> GetAuctionsByCategory(int categoryId)
        {
            var auctionCount = await _categoryStatsService.GetAuctionsByCategoryAsync(categoryId);
            return Ok(auctionCount);
        }

        /// <summary>
        /// Gets the most popular category based on the number of auctions.
        /// </summary>
        /// <returns>The name of the most popular category.</returns>
        [HttpGet("most-popular-category")]
        public async Task<ActionResult<string>> GetMostPopularCategory()
        {
            var mostPopularCategory = await _categoryStatsService.GetMostPopularCategoryAsync();
            return Ok(mostPopularCategory);
        }

        /// <summary>
        /// Gets the distribution of auctions across all categories.
        /// </summary>
        /// <returns>A dictionary with category IDs as keys and auction counts as values.</returns>
        [HttpGet("auction-distribution")]
        public async Task<ActionResult<Dictionary<int, int>>> GetAuctionDistributionByCategory()
        {
            var auctionDistribution = await _categoryStatsService.GetAuctionDistributionByCategoryAsync();
            return Ok(auctionDistribution);
        }
    }
}
