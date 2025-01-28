using AuctionProject.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Url { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public string? ManagerId { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

        public static ItemDTO ItemToDTO(Item item)
        {
            return new ItemDTO(
                item.Name,
                item.Url,
                item.Description,
                item.Categories.Select(c => c.Id).ToList(),
                item.ManagerId!
            );
        }

        public static List<ItemDTO> ItemListToDTO(IEnumerable<Item> items)
        {
            return items.Select(ItemToDTO).ToList();
        }
    }
}
