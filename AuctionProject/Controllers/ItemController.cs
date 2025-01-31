using AuctionProject.Models.DTOs;
using AuctionProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> AddItem(ItemDTO itemDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state.");
            }

            try
            {
                var createdItem = await _itemService.AddItemAsync(itemDTO);

                return CreatedAtAction(nameof(GetItemById), new { id = createdItem.Id }, createdItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Item/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var item = await _itemService.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ItemDTO>>> GetAllItems()
        {
            try
            {
                var items = await _itemService.GetAllItemsAsync();

                if (items == null || !items.Any())
                {
                    return NotFound("No items found.");
                }

                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves items that belong to a specific category.
        /// </summary>
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<ItemDTO>>> GetItemsByCategoryId(int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest("Invalid category ID.");
            }

            try
            {
                var items = await _itemService.GetItemsByCategoryIdAsync(categoryId);

                if (items == null || !items.Any())
                {
                    return NotFound($"No items found for category ID {categoryId}.");
                }

                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing item based on the provided ID and updated details.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDTO>> UpdateItem(int id, ItemDTO updatedItem)
        {
            if (id <= 0 || updatedItem == null)
            {
                return BadRequest("Invalid parameters.");
            }

            try
            {
                var item = await _itemService.UpdateItemAsync(id, updatedItem);

                if (item == null)
                {
                    return NotFound($"Item with ID {id} not found.");
                }

                return Ok(item);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Item/{id}/status
        [HttpPut("{id}/status")]
        public async Task<ActionResult<ItemDTO>> MarkItemAsSold(int id)
        {
            try
            {
                var updatedItem = await _itemService.MarkItemAsSoldAsync(id);

                if (updatedItem == null)
                {
                    return NotFound();
                }

                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Item/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var deleted = await _itemService.DeleteItemAsync(id);

            if (!deleted)
            {
                return NotFound($"Item with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
