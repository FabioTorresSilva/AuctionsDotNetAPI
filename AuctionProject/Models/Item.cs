using AuctionProject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuctionProject.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        public String Url { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public List<Category> Categories { get; set; } = new List<Category>();
        
        [Required]
        public string ManagerId { get; set; }

    }
}
