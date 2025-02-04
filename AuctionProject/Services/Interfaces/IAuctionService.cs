using AuctionProject.Models.DTOs;

namespace AuctionProject.Services.Interfaces
{
    /// <summary>
    /// Interface that defines auction-related business operations.
    /// </summary>
    public interface IAuctionService
    {
        /// <summary>
        /// Creates a new auction.
        /// </summary>
        Task<AuctionDTO> CreateAuctionAsync(AuctionDTO auctionDTO);

        /// <summary>
        /// Retrieves an auction by its ID.
        /// </summary>
        Task<AuctionDTO?> GetAuctionByIdAsync(int id);

        /// <summary>
        /// Retrieves all auctions.
        /// </summary>
        Task<List<AuctionDTO>> GetAllAuctionsAsync();

        /// <summary>
        /// Retrieves auctions by the item ID.
        /// </summary>
        Task<List<AuctionDTO>> GetAuctionsByItemIdAsync(int itemId);

        /// <summary>
        /// Updates an existing auction.
        /// </summary>
        Task<AuctionDTO?> UpdateAuctionAsync(int id, AuctionDTO auctionDTO);

        /// <summary>
        /// Deletes an auction by its ID.
        /// </summary>
        Task<bool> DeleteAuctionAsync(int id);

        /// <summary>
        /// Retrieves auctions by the manager ID.
        /// </summary>
        Task<List<AuctionDTO>> GetAuctionsByManagerIdAsync(int managerId);

        /// <summary>
        /// Updates the status of an auction and records its sold value.
        /// </summary>
        Task<AuctionDTO?> UpdateAuctionStatusAsync(int id, decimal soldValue);

        Task <List<AuctionDTO>> GetOpenAuctions();
    }
}
