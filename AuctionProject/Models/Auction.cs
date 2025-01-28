using AuctionProject.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionProject.Models
{
    public class Auction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Item")]
        public int ItemId { get; set; }

        [Required]
        public AuctionType AuctionType { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Starting price must be greater than zero.")]
        public decimal StartingPrice { get; set; }

        [Required]
        public DateTime StartDate { get; set; } 

        [Required]
        public DateTime EndDate { get; set; } 

        [MaxLength(50)]
        public string Status { get; set; } = "Open"; 

        [Required]
        public string ManagerId { get; set; } = null!;
    }
}
