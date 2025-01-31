
namespace AuctionProject.Services.Interfaces
{
    /// <summary>
    /// Service that updates the statuses of auctions.
    /// </summary>
    public interface IAuctionStatusUpdateService
    {
        /// <summary>
        /// Updates the statuses of auctions based on predefined business logic.
        /// This could involve marking auctions as completed, closed, or any other status updates.
        /// </summary>
        Task UpdateAuctionStatusesAsync();
    }
}
