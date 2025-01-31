using AuctionProject.Models.DTOs;
using AuctionProject.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuctionProject.Models
{
    /// <summary>
    /// Represents an auction in the auction system, including details such as item, pricing, dates, status, and associated manager.
    /// </summary>
    public class Auction
    {
        /// <summary>
        /// Gets or sets the unique identifier for the auction.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the item associated with the auction.
        /// </summary>
        [Required]
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets the item associated with the auction.
        /// </summary>
        [Required]
        public Item Item { get; set; } = null!;

        /// <summary>
        /// Gets or sets the auction type (e.g., open, closed).
        /// </summary>
        [Required]
        public AuctionType AuctionType { get; set; }

        /// <summary>
        /// Gets or sets the starting price for the auction. Must be greater than zero.
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Starting price must be greater than zero.")]
        public decimal StartingPrice { get; set; }

        /// <summary>
        /// Gets or sets the start date of the auction.
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the auction.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the auction (e.g., Pending, Open, Sold, Closed).
        /// Default is set to "Pending".
        /// </summary>
        [MaxLength(50)]
        public AuctionStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the manager responsible for this auction.
        /// </summary>
        [Required]
        public int ManagerId { get; set; }

        /// <summary>
        /// Gets or sets the value the item was sold for (if applicable).
        /// </summary>
        public decimal SoldValue { get; set; }

        /// <summary>
        /// Gets or sets the manager associated with this auction.
        /// </summary>
        public Manager Manager { get; set; }

        /// <summary>
        /// Converts an Auction object to an AuctionDTO.
        /// </summary>
        /// <param name="auction">The Auction object to convert.</param>
        /// <returns>A new AuctionDTO representing the auction's data.</returns>
        public static AuctionDTO AuctionToDTO(Auction auction)
        {
            return new AuctionDTO
            {
                Id = auction.Id,
                ItemId = auction.ItemId,
                AuctionType = auction.AuctionType,
                StartingPrice = auction.StartingPrice,
                StartDate = auction.StartDate,
                EndDate = auction.EndDate,
                Status = auction.Status,
                SoldValue = auction.SoldValue,
                ManagerId = auction.ManagerId
            };
        }

        /// <summary>
        /// Converts a collection of Auction objects to a list of AuctionDTOs.
        /// </summary>
        /// <param name="auctions">The collection of Auction objects to convert.</param>
        /// <returns>A list of AuctionDTOs representing the auctions.</returns>
        public static List<AuctionDTO> AuctionListToDTO(IEnumerable<Auction> auctions)
        {
            return auctions.Select(AuctionToDTO).ToList();
        }
    }
}
