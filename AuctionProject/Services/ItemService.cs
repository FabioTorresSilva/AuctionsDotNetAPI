using AuctionProject.Data;
using AuctionProject.Models;
using AuctionProject.Models.DTOs;
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
            Console.WriteLine(itemDTO.CategoryIds);
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
                ManagerId = itemDTO.ManagerId
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return Item.ItemToDTO(item);
        }


        public async Task<ItemDTO?> GetItemByIdAsync(int id)
{
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

    return items.Select(Item.ItemToDTO).ToList();
}

public async Task<List<ItemDTO>> GetItemsByCategoryIdAsync(int categoryId)
{
    var items = await _context.Items
        .Include(i => i.Categories)
        .Where(i => i.Categories.Any(c => c.Id == categoryId))
        .ToListAsync();

    return items.Select(Item.ItemToDTO).ToList();
}

public async Task<ItemDTO?> UpdateItemAsync(int id, ItemDTO updatedItem)
{
    var item = await _context.Items
        .Include(i => i.Categories)
        .FirstOrDefaultAsync(i => i.Id == id);

    if (item == null) return null;

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

public async Task<bool> DeleteItemAsync(int id)
{
    var item = await _context.Items.FindAsync(id);

    if (item == null) return false;

    _context.Items.Remove(item);
    await _context.SaveChangesAsync();

    return true;
}
    }
}
