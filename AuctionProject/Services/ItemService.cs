using AuctionProject.Data;
using AuctionProject.Models;
using AuctionProject.Models.DTOs;
using AuctionProject.Models.Enums;
using AuctionProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionProject.Services
{
    public class ItemService : IItemService
    {
        private readonly AuctionContext _context;

        public ItemService(AuctionContext context)
        {
            _context = context;
        }

        public async Task<ItemDTO> AddItemAsync(ItemDTO itemDTO)
        {
            if (string.IsNullOrWhiteSpace(itemDTO.Name))
            {
                throw new ArgumentException("Item name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(itemDTO.Url))
            {
                throw new ArgumentException("Item URL cannot be empty.");
            }

            var categories = await _context.Categories
                .Where(c => itemDTO.CategoryIds.Contains(c.Id))
                .ToListAsync();

            if (!categories.Any())
            {
                throw new ArgumentException("Invalid category IDs provided.");
            }

            var item = new Item
            {
                Name = itemDTO.Name,
                Url = itemDTO.Url,
                Description = itemDTO.Description,
                Categories = categories,
                ManagerId = itemDTO.ManagerId,
                Status = ItemStatus.Available
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return Item.ItemToDTO(item);
        }

        public async Task<ItemDTO?> GetItemByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid item ID.");
            }

            var item = await _context.Items
                .Include(i => i.Categories)
                .FirstOrDefaultAsync(i => i.Id == id);

            return item == null ? null : Item.ItemToDTO(item);
        }


        public async Task<List<ItemDTO>> GetAllItemsAsync()
        {
            var items = await _context.Items
                .Include(i => i.Categories)
                .ToListAsync();

            if (items == null || !items.Any())
            {
                throw new InvalidOperationException("No items found.");
            }

            return items.Select(Item.ItemToDTO).ToList();
        }

        public async Task<List<ItemDTO>> GetItemsByCategoryIdAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("Invalid category ID.");
            }

            var items = await _context.Items
                .Include(i => i.Categories)
                .Where(i => i.Categories.Any(c => c.Id == categoryId))
                .ToListAsync();

            if (items == null || !items.Any())
            {
                throw new InvalidOperationException("No items found for the specified category.");
            }

            return items.Select(Item.ItemToDTO).ToList();
        }

        public async Task<ItemDTO?> UpdateItemAsync(int id, ItemDTO updatedItem)
        {
            if (id <= 0 || updatedItem == null)
            {
                throw new ArgumentException("Invalid input parameters.");
            }

            var item = await _context.Items
                .Include(i => i.Categories)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return null;

            if (item.Status == ItemStatus.Sold)
            {
                throw new ArgumentException("You cant update a Sold item");
            }

            var categories = await _context.Categories
                .Where(c => updatedItem.CategoryIds.Contains(c.Id))
                .ToListAsync();

            item.Name = updatedItem.Name;
            item.Url = updatedItem.Url;
            item.Description = updatedItem.Description;
            item.Categories = categories;
            item.ManagerId = updatedItem.ManagerId;

            await _context.SaveChangesAsync();

            return Item.ItemToDTO(item);
        }

        public async Task<ItemDTO?> MarkItemAsSoldAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid item ID.");
            }

            var item = await _context.Items.FindAsync(id);

            if (item == null) return null;

            if (item.Status == ItemStatus.Sold)
            {
                throw new ArgumentException("You cant change a Sold status");
            }

            item.Status = ItemStatus.Sold;

            await _context.SaveChangesAsync();

            return Item.ItemToDTO(item);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid item ID.");
            }

            var item = await _context.Items.FindAsync(id);

            if (item == null) return false;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
