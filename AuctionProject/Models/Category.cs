using System.ComponentModel.DataAnnotations;

namespace AuctionProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();
    }
}
