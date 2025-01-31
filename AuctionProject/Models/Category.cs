using AuctionProject.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace AuctionProject.Models
{
    /// <summary>
    /// Represents a category that items can belong to in the auction system.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the category. The name is required and has a maximum length of 100 characters.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Navigation property for the items that belong to this category.
        /// </summary>
        public ICollection<Item> Items { get; set; } = new List<Item>();

        /// <summary>
        /// Converts a Category object to a CategoryDTO.
        /// </summary>
        /// <param name="category">The Category object to convert.</param>
        /// <returns>A CategoryDTO containing the Id and Name of the category.</returns>
        public static CategoryDTO CategoryToDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        /// <summary>
        /// Converts a collection of Category objects to a list of CategoryDTOs.
        /// </summary>
        /// <param name="categories">The collection of Category objects to convert.</param>
        /// <returns>A list of CategoryDTOs representing the categories.</returns>
        public static List<CategoryDTO> CategoryListToDTO(IEnumerable<Category> categories)
        {
            return categories.Select(CategoryToDTO).ToList();
        }
    }
}