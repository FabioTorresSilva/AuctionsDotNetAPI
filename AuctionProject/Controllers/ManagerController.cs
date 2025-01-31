using AuctionProject.Models.DTOs;
using AuctionProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuctionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        /// <summary>
        /// Creates a new manager in the system.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ManagerDTO>> AddManager(ManagerDTO managerDTO)
        {
            try
            {
                var createdManager = await _managerService.AddManagerAsync(managerDTO);

                return CreatedAtAction(nameof(GetManagerById), new { id = createdManager.Id }, createdManager);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a manager by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ManagerDTO>> GetManagerById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid manager ID.");
            }

            try
            {
                var manager = await _managerService.GetManagerByIdAsync(id);

                if (manager == null)
                {
                    return NotFound($"Manager with ID {id} not found.");
                }

                return Ok(manager);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all managers.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ManagerDTO>>> GetAllManagers()
        {
            var managers = await _managerService.GetAllManagersAsync();
            return Ok(managers);
        }

        // PUT: api/Manager/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ManagerDTO>> UpdateManager(int id, ManagerDTO updatedManager)
        {
            try
            {
                var manager = await _managerService.UpdateManagerAsync(id, updatedManager);

                if (manager == null)
                {
                    return NotFound();
                }

                return Ok(manager);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a manager by its ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteManager(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid manager ID.");
            }
            try
            {
                var deleted = await _managerService.DeleteManagerAsync(id);

                if (!deleted)
                {
                    return NotFound($"Manager with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
