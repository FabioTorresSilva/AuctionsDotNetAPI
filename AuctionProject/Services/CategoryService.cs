using AuctionProject.Data;
using AuctionProject.Models;
using AuctionProject.Models.DTOs;
using AuctionProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AuctionContext _context;

        public CategoryService(AuctionContext context)
        {
            _context = context;
        }

        // Get Category by ID
        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return null;
            }

            return Category.CategoryToDTO(category);
        }

        public async Task<CategoryDTO> GetCategoryByNameAsync(string name)
        {
            var category = await _context.Categories
                .Where(c => c.Name == name)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return null;
            }

            return Category.CategoryToDTO(category);
        }

        // Get All Categories
        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                .ToListAsync();

            return Category.CategoryListToDTO(categories);
        }

        // Create a new Category
        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Category.CategoryToDTO(category);
        }

        // Update an existing Category
        public async Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryDTO categoryDto)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return null;
            }

            category.Name = categoryDto.Name;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return Category.CategoryToDTO(category);
        }

        // Delete a Category by ID
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
