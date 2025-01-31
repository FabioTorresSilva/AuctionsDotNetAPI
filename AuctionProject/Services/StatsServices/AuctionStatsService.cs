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
    /// Service that provides methods for retrieving auction-related statistics.
    /// </summary>
    public class AuctionStatsService : IAuctionStats
    {
        private readonly AuctionContext _context;

        public AuctionStatsService(AuctionContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalAuctionsCreatedAsync()
        {
            return await _context.Auctions.CountAsync();
        }

        public async Task<int> GetActiveAuctionsAsync()
        {
            // Count auctions that are currently open or in progress
            return await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Open || a.Status == AuctionStatus.Pending)
                .CountAsync();
        }

        public async Task<int> GetAuctionsClosedAsync()
        {
            // Count auctions that have been closed
            return await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Close)
                .CountAsync();
        }

        public async Task<int> GetAuctionsSoldAsync()
        {
            // Count auctions that have been sold
            return await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Sold)
                .CountAsync();
        }

        public async Task<int> GetAuctionsNotSoldAsync()
        {
            // Count auctions where the item was not sold
            return await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Close && a.SoldValue < a.StartingPrice)
                .CountAsync();
        }

        public async Task<Dictionary<string, int>> GetAuctionStatusDistributionAsync()
        {
            var statusDistribution = await _context.Auctions
                .GroupBy(a => a.Status.ToString())
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return statusDistribution.ToDictionary(x => x.Status, x => x.Count);
        }

        public async Task<Dictionary<int, int>> GetAuctionsByManagerAsync()
        {
            var managerStats = await _context.Auctions
                .GroupBy(a => a.ManagerId)
                .Select(g => new { ManagerId = g.Key, Count = g.Count() })
                .ToListAsync();

            return managerStats.ToDictionary(x => x.ManagerId, x => x.Count);
        }
    }
}
