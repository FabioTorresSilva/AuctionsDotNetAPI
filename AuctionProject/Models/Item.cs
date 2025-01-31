using AuctionProject.Models.DTOs;
using AuctionProject.Models;
using System.ComponentModel.DataAnnotations;
using AuctionProject.Models.Enums;

namespace AuctionProject.Models
{
    /// <summary>
    /// Represents an item in the auction system.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item. The name is required and has a maximum length of 100 characters.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL associated with the item. This is a required property.
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the description of the item. The description is optional and can be up to 500 characters long.
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the manager responsible for the item.
        /// </summary>
        [Required]
        public int ManagerId { get; set; }

        /// <summary>
        /// Navigation property to the manager associated with this item.
        /// </summary>
        public Manager Manager { get; set; }

        /// <summary>
        /// Gets or sets the status of the item. This is a required property.
        /// </summary>
        [Required]
        public ItemStatus Status { get; set; }

        /// <summary>
        /// Navigation property to the categories associated with this item.
        /// </summary>
        public List<Category> Categories { get; set; } = new List<Category>();

        /// <summary>
        /// Converts an Item object to an ItemDTO.
        /// </summary>
        /// <param name="item">The Item object to convert.</param>
        /// <returns>An ItemDTO containing the Id, Name, Url, Description, CategoryIds, ManagerId, and Status of the item.</returns>
        public static ItemDTO ItemToDTO(Item item)
        {
            return new ItemDTO(item.Id, item.Name, item.Url, item.Description,
                item.Categories.Select(c => c.Id).ToList(), item.ManagerId, item.Status.ToString());
        }

        /// <summary>
        /// Converts a collection of Item objects to a list of ItemDTOs.
        /// </summary>
        /// <param name="items">The collection of Item objects to convert.</param>
        /// <returns>A list of ItemDTOs representing the items.</returns>
        public static List<ItemDTO> ToDTOList(IEnumerable<Item> items)
        {
            return items.Select(ItemToDTO).ToList();
        }

    }
}
