using System.ComponentModel.DataAnnotations;

namespace AuctionProject.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for transferring item data.
    /// </summary>
    public class ItemDTO
    {
        /// <summary>
        /// The unique identifier for the item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The URL pointing to the item's listing or additional details.
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// A brief description of the item (optional).
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// List of category IDs that the item belongs to.
        /// </summary>
        public List<int> CategoryIds { get; set; } = new List<int>();

        /// <summary>
        /// The ID of the manager responsible for the item.
        /// </summary>
        [Required]
        public int ManagerId { get; set; }

        /// <summary>
        /// The current status of the item (e.g., Available or Sold).
        /// </summary>
        public string Status { get; set; } = "Available";  // Default to "Available"

        /// <summary>
        /// Parameterless constructor for deserialization (e.g., used by JSON deserialization).
        /// </summary>
        public ItemDTO() { }

        /// <summary>
        /// Parameterized constructor to initialize an ItemDTO.
        /// </summary>
        public ItemDTO(int id, string name, string url, string? description, List<int> categoryIds, int managerId, string status = "Available")
        {
            Id = id;
            Name = name;
            Url = url;
            Description = description;
            CategoryIds = categoryIds;
            ManagerId = managerId;
            Status = status;
        }
    }
}
