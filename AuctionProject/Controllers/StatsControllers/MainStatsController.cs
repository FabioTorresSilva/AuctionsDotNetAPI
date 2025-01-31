using AuctionProject.Services.Stats.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionProject.Controllers
{
    /// <summary>
    /// Controller to expose key financial and performance statistics for auctions.
    /// </summary>
    [Route("api/stats/main")]
    [ApiController]
    public class MainStatsController : ControllerBase
    {
        private readonly IMainStats _mainStatsService;

        public MainStatsController(IMainStats mainStatsService)
        {
            _mainStatsService = mainStatsService;
        }

        /// <summary>
        /// Gets the total revenue from sold auctions.
        /// </summary>
        [HttpGet("total-revenue")]
        public async Task<ActionResult<decimal>> GetTotalRevenueAsync()
        {
            var result = await _mainStatsService.GetTotalRevenueAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the average sale price of sold auctions.
        /// </summary>
        [HttpGet("average-sale-price")]
        public async Task<ActionResult<decimal>> GetAverageSalePriceAsync()
        {
            var result = await _mainStatsService.GetAverageSalePriceAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the total profit from sold auctions.
        /// </summary>
        [HttpGet("total-profit")]
        public async Task<ActionResult<decimal>> GetTotalProfitAsync()
        {
            var result = await _mainStatsService.GetTotalProfitAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the average profit per auction.
        /// </summary>
        [HttpGet("average-profit")]
        public async Task<ActionResult<decimal>> GetAverageProfitPerAuctionAsync()
        {
            var result = await _mainStatsService.GetAverageProfitPerAuctionAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves the auction with the highest profit.
        /// </summary>
        [HttpGet("highest-profit-auction")]
        public async Task<ActionResult<object>> GetHighestProfitAuctionAsync()
        {
            var result = await _mainStatsService.GetHighestProfitAuctionAsync();
            return result != null ? Ok(result) : NotFound("No auctions found.");
        }

       

        /// <summary>
        /// Gets the highest sale price recorded.
        /// </summary>
        [HttpGet("highest-sale-price")]
        public async Task<ActionResult<decimal>> GetHighestSalePriceAsync()
        {
            var result = await _mainStatsService.GetHighestSalePriceAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the lowest sale price recorded.
        /// </summary>
        [HttpGet("lowest-sale-price")]
        public async Task<ActionResult<decimal>> GetLowestSalePriceAsync()
        {
            var result = await _mainStatsService.GetLowestSalePriceAsync();
            return Ok(result);
        }
    }
}
