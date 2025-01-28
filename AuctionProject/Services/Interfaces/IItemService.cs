using AuctionProject.Models.DTOs;

namespace AuctionProject.Services.Interfaces
{
    public interface IItemService
    {
        // Create
        Task<ItemDTO> AddItemAsync(ItemDTO itemDTO);

        // Read
        Task<ItemDTO?> GetItemByIdAsync(int id);
        Task<List<ItemDTO>> GetAllItemsAsync();
        Task<List<ItemDTO>> GetItemsByCategoryIdAsync(int categoryId);

        // Update
        Task<ItemDTO?> UpdateItemAsync(int id, ItemDTO updatedItem);

        // Delete
        Task<bool> DeleteItemAsync(int id);
    }
}
