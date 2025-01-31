using AuctionProject.Models.DTOs;

namespace AuctionProject.Services.Interfaces
{
    /// <summary>
    /// Service interface for managing items.
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        /// Adds a new item.
        /// </summary>
        Task<ItemDTO> AddItemAsync(ItemDTO itemDTO);

        /// <summary>
        /// Retrieves an item by its ID.
        /// </summary>
        Task<ItemDTO?> GetItemByIdAsync(int id);

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        Task<List<ItemDTO>> GetAllItemsAsync();

        /// <summary>
        /// Retrieves all items by category ID.
        /// </summary>
        Task<List<ItemDTO>> GetItemsByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        Task<ItemDTO?> UpdateItemAsync(int id, ItemDTO updatedItem);

        /// <summary>
        /// Marks an item as sold.
        /// </summary>
        Task<ItemDTO?> MarkItemAsSoldAsync(int id);

        /// <summary>
        /// Deletes an item by its ID.
        /// </summary>
        Task<bool> DeleteItemAsync(int id);
    }
}
