using AuctionProject.Models.DTOs;
using AuctionProject.Services.Stats.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionProject.Controllers
{
    /// <summary>
    /// Controller to expose auction-related statistics.
    /// </summary>
    [Route("api/stats/auctions")]
    [ApiController]
    public class AuctionStatsController : ControllerBase
    {
        private readonly IAuctionStats _auctionStatsService;

        public AuctionStatsController(IAuctionStats auctionStatsService)
        {
            _auctionStatsService = auctionStatsService;
        }

        /// <summary>
        /// Get total number of auctions created.
        /// </summary>
        /// <returns>Total auctions created.</returns>
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalAuctionsCreatedAsync()
        {
            var result = await _auctionStatsService.GetTotalAuctionsCreatedAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get the number of active auctions.
        /// </summary>
        /// <returns>Active auctions count.</returns>
        [HttpGet("active")]
        public async Task<ActionResult<int>> GetActiveAuctionsAsync()
        {
            var result = await _auctionStatsService.GetActiveAuctionsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get the number of auctions that have been closed.
        /// </summary>
        /// <returns>Closed auctions count.</returns>
        [HttpGet("closed")]
        public async Task<ActionResult<int>> GetAuctionsClosedAsync()
        {
            var result = await _auctionStatsService.GetAuctionsClosedAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get the number of auctions that have been sold.
        /// </summary>
        /// <returns>Sold auctions count.</returns>
        [HttpGet("sold")]
        public async Task<ActionResult<int>> GetAuctionsSoldAsync()
        {
            var result = await _auctionStatsService.GetAuctionsSoldAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get the number of auctions that were not sold.
        /// </summary>
        /// <returns>Not sold auctions count.</returns>
        [HttpGet("not-sold")]
        public async Task<ActionResult<int>> GetAuctionsNotSoldAsync()
        {
            var result = await _auctionStatsService.GetAuctionsNotSoldAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get the distribution of auction statuses.
        /// </summary>
        /// <returns>Dictionary of auction statuses and their counts.</returns>
        [HttpGet("status-distribution")]
        public async Task<ActionResult<Dictionary<string, int>>> GetAuctionStatusDistributionAsync()
        {
            var result = await _auctionStatsService.GetAuctionStatusDistributionAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get the number of auctions per manager.
        /// </summary>
        /// <returns>Dictionary of manager IDs and the number of auctions they managed.</returns>
        [HttpGet("auctions-by-manager")]
        public async Task<ActionResult<Dictionary<int, int>>> GetAuctionsByManagerAsync()
        {
            var result = await _auctionStatsService.GetAuctionsByManagerAsync();
            return Ok(result);
        }
    }
}
