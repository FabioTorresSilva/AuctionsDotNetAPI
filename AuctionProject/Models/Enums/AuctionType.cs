namespace AuctionProject.Models.Enums
{
    /// <summary>
    /// Represents the different types of auctions available in the system.
    /// </summary>
    public enum AuctionType
    {
        /// <summary>
        /// An open auction where bids are publicly visible to all participants.
        /// </summary>
        OpenAuction,

        /// <summary>
        /// A sealed bid auction where bids are not visible to other participants.
        /// </summary>
        SealedBid
    }
}
