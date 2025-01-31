using AuctionProject.Models.DTOs;

namespace AuctionProject.Services.Stats.Interfaces
{
    /// <summary>
    /// Provides methods for retrieving auction-related statistics.
    /// </summary>
    public interface IAuctionStats
    {
        /// <summary>
        /// Gets the total number of auctions created over a specific period (e.g., daily, weekly, monthly).
        /// </summary>
        /// <returns>The total number of auctions created.</returns>
        Task<int> GetTotalAuctionsCreatedAsync();

        /// <summary>
        /// Gets the number of auctions that are currently open or in progress.
        /// </summary>
        /// <returns>The count of active auctions.</returns>
        Task<int> GetActiveAuctionsAsync();

        /// <summary>
        /// Gets the number of auctions that have been successfully closed.
        /// </summary>
        /// <returns>The count of closed auctions.</returns>
        Task<int> GetAuctionsClosedAsync();

        /// <summary>
        /// Gets the number of auctions where the item has been successfully sold.
        /// </summary>
        /// <returns>The count of auctions that resulted in a sale.</returns>
        Task<int> GetAuctionsSoldAsync();

        /// <summary>
        /// Gets the number of auctions where the item was not sold.
        /// </summary>
        /// <returns>The count of auctions that did not result in a sale.</returns>
        Task<int> GetAuctionsNotSoldAsync();

        /// <summary>
        /// Gets a distribution of auctions by their current status (e.g., Open, Pending, Sold).
        /// </summary>
        /// <returns>A dictionary of auction statuses and their counts.</returns>
        Task<Dictionary<string, int>> GetAuctionStatusDistributionAsync();

        /// <summary>
        /// Gets the number of auctions managed by each individual manager.
        /// </summary>
        /// <returns>A dictionary of manager IDs and the corresponding number of auctions they manage.</returns>
        Task<Dictionary<int, int>> GetAuctionsByManagerAsync();
    }
}
