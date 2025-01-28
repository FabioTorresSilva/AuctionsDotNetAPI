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

        // POST: api/Item
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> AddItem(ItemDTO itemDTO)
        {
            try
            {
                var createdItem = await _itemService.AddItemAsync(itemDTO);

                // Return 201 Created with location header
                return CreatedAtAction(nameof(GetItemById), new { id = createdItem.Id }, createdItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Item/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemById(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<List<ItemDTO>>> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        // GET: api/Item/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<ItemDTO>>> GetItemsByCategoryId(int categoryId)
        {
            var items = await _itemService.GetItemsByCategoryIdAsync(categoryId);

            if (items == null || !items.Any())
            {
                return NotFound();
            }

            return Ok(items);
        }

        // PUT: api/Item/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDTO>> UpdateItem(int id, ItemDTO updatedItem)
        {
            try
            {
                var item = await _itemService.UpdateItemAsync(id, updatedItem);

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
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
            var deleted = await _itemService.DeleteItemAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent(); // Return 204 No Content on successful deletion
        }
    }
}
