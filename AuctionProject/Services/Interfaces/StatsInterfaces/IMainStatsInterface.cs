using AuctionProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionProject.Services.Stats.Interfaces
{
    /// <summary>
    /// Interface for key financial and performance statistics in the auction system.
    /// </summary>
    public interface IMainStats
    {
        /// <summary>
        /// Gets the total revenue from all sold auctions.
        /// </summary>
        Task<decimal> GetTotalRevenueAsync();

        /// <summary>
        /// Gets the average sale price of sold auctions.
        /// </summary>
        Task<decimal> GetAverageSalePriceAsync();

        /// <summary>
        /// Gets the total profit from all sold auctions.
        /// </summary>
        Task<decimal> GetTotalProfitAsync();

        /// <summary>
        /// Gets the average profit per auction.
        /// </summary>
        Task<decimal> GetAverageProfitPerAuctionAsync();

        /// <summary>
        /// Retrieves the auction with the highest profit.
        /// </summary>
        Task<AuctionDTO?> GetHighestProfitAuctionAsync();

        /// <summary>
        /// Retrieves the highest sale price recorded in an auction.
        /// </summary>
        Task<decimal> GetHighestSalePriceAsync();

        /// <summary>
        /// Retrieves the lowest sale price recorded in an auction.
        /// </summary>
        Task<decimal> GetLowestSalePriceAsync();
    }
}
