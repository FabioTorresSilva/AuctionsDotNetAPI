namespace AuctionProject.Models.Enums
{
    /// <summary>
    /// Represents the various statuses an auction can have.
    /// </summary>
    public enum AuctionStatus
    {
        /// <summary>
        /// The auction is pending and has not yet started.
        /// </summary>
        Pending,

        /// <summary>
        /// The auction is open and active, accepting bids.
        /// </summary>
        Open,

        /// <summary>
        /// The auction has been closed and no longer accepts bids.
        /// </summary>
        Close,

        /// <summary>
        /// The auction has been sold, and the item has been purchased.
        /// </summary>
        Sold
    }
}
