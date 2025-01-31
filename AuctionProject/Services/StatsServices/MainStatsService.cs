using AuctionProject.Data;
using AuctionProject.Models;
using AuctionProject.Models.DTOs;
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
    /// Service that provides key financial and performance statistics for auctions.
    /// </summary>
    public class MainStatsService : IMainStats
    {
        private readonly AuctionContext _context;

        public MainStatsService(AuctionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Calculates the total revenue from sold auctions.
        /// </summary>
        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Sold)
                .SumAsync(a => a.SoldValue);
        }

        /// <summary>
        /// Gets the average sale price of sold auctions.
        /// </summary>
        public async Task<decimal> GetAverageSalePriceAsync()
        {
            var totalSold = await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Sold)
                .Select(a => a.SoldValue)
                .ToListAsync();

            return totalSold.Any() ? totalSold.Average() : 0m;
        }

        /// <summary>
        /// Calculates total profit as the difference between sold price and starting price.
        /// </summary>
        public async Task<decimal> GetTotalProfitAsync()
        {
            return await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Sold)
                .SumAsync(a => a.SoldValue - a.StartingPrice);
        }

        /// <summary>
        /// Gets the average profit per auction.
        /// </summary>
        public async Task<decimal> GetAverageProfitPerAuctionAsync()
        {
            var profits = await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Sold)
                .Select(a => a.SoldValue - a.StartingPrice)
                .ToListAsync();

            return profits.Any() ? profits.Average() : 0m;
        }

        /// <summary>
        /// Retrieves the auction with the highest profit.
        /// </summary>
        public async Task<AuctionDTO?> GetHighestProfitAuctionAsync()
        {
            var highestProfitAuction = await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Sold)
                .OrderByDescending(a => a.SoldValue - a.StartingPrice)
                .FirstOrDefaultAsync();

            return highestProfitAuction != null ? Auction.AuctionToDTO(highestProfitAuction) : null;
        }

        /// <summary>
        /// Gets the percentage of auctions that sold above the starting price.
        /// </summary>
        public async Task<double> GetAuctionsAboveStartingPriceRateAsync()
        {
            var totalSold = await _context.Auctions.CountAsync(a => a.Status == AuctionStatus.Sold);
            var soldAboveStarting = await _context.Auctions.CountAsync(a => a.Status == AuctionStatus.Sold && a.SoldValue > a.StartingPrice);

            return totalSold > 0 ? (double)soldAboveStarting / totalSold * 100 : 0;
        }

        /// <summary>
        /// Retrieves the highest sale price recorded in an auction.
        /// </summary>
        public async Task<decimal> GetHighestSalePriceAsync()
        {
            return await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Sold)
                .MaxAsync(a => (decimal?)a.SoldValue) ?? 0m;
        }

        /// <summary>
        /// Retrieves the lowest sale price recorded in an auction.
        /// </summary>
        public async Task<decimal> GetLowestSalePriceAsync()
        {
            return await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Sold)
                .MinAsync(a => (decimal?)a.SoldValue) ?? 0m;
        }
    }
}
