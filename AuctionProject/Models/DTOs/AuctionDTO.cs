using AuctionProject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuctionProject.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for transferring auction data.
    /// </summary>
    public class AuctionDTO
    {
        /// <summary>
        /// The unique identifier for the auction.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ID of the item being auctioned.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// The type of auction (e.g., Open Auction, Sealed Bid).
        /// </summary>
        public AuctionType AuctionType { get; set; }

        /// <summary>
        /// The starting price for the auction.
        /// </summary>
        /// <remarks>
        /// The price must be greater than zero.
        /// </remarks>
        [Range(0.01, double.MaxValue, ErrorMessage = "Starting price must be greater than zero.")]
        public decimal StartingPrice { get; set; }

        /// <summary>
        /// The date and time when the auction starts.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The date and time when the auction ends.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The status of the auction (e.g., Pending, Open, Closed, Sold).
        /// </summary>
        [Required]
        public AuctionStatus Status { get; set; }

        /// <summary>
        /// The final value of the auction if it was sold.
        /// </summary>
        /// <remarks>
        /// This field is populated if the auction is successfully sold.
        /// </remarks>
        public decimal SoldValue { get; set; }

        /// <summary>
        /// The ID of the manager who created the auction.
        /// </summary>
        public int ManagerId { get; set; }
    }
}
