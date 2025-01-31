using AuctionProject.Models.DTOs;

namespace AuctionProject.Services.Interfaces
{
    /// <summary>
    /// Service interface for handling categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        Task<CategoryDTO> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Retrieves a category by its name.
        /// </summary>
        Task<CategoryDTO> GetCategoryByNameAsync(string name);

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();

        /// <summary>
        /// Creates a new category.
        /// </summary>
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto);

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryDTO categoryDto);

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        Task<bool> DeleteCategoryAsync(int id);
    }
}
