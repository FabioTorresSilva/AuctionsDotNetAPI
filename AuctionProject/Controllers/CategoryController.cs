using AuctionProject.Models.DTOs;
using AuctionProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving categories.");
            }
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);

                if (category == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the category.");
            }
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null || string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                return BadRequest("Category data is required and must have a valid name.");
            }

            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);

                return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the category.");
            }
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null || string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                return BadRequest("Category data is required and must have a valid name.");
            }

            try
            {
                var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);

                if (updatedCategory == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }

                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the category.");
            }
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);

                if (!result)
                {
                    return NotFound($"Category with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the category.");
            }
        }

        /// <summary>
        /// Retrieves a category by its name.
        /// </summary>
        [HttpGet("name/{name}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Category name cannot be empty.");
            }

            var category = await _categoryService.GetCategoryByNameAsync(name);

            if (category == null)
            {
                return NotFound($"No category found with name '{name}'.");
            }

            return Ok(category);
        }
    }
}
