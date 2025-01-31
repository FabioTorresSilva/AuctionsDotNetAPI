namespace AuctionProject.Models.Enums
{
    /// <summary>
    /// Represents the possible statuses of an item in the auction system.
    /// </summary>
    public enum ItemStatus
    {
        /// <summary>
        /// The item is available for auction and has not yet been sold.
        /// </summary>
        Available = 1,  

        /// <summary>
        /// The item has been sold in an auction.
        /// </summary>
        Sold = 2,    
    }
}
