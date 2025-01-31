using AuctionProject.Models.DTOs;
using AuctionProject.Models.Enums;
using AuctionProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionProject.Controllers
{
    /// <summary>
    /// Controller responsible for handling API requests related to auctions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        /// <summary>
        /// Creates a new auction based on the provided auction data.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AuctionDTO>> CreateAuction([FromBody] AuctionDTO auctionDTO)
        {
            if (auctionDTO == null) return BadRequest("Auction data is required.");

            try
            {
                var createdAuction = await _auctionService.CreateAuctionAsync(auctionDTO);

                return CreatedAtAction(nameof(CreateAuction), new { id = createdAuction.Id }, createdAuction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


        /// <summary>
        /// Retrieves an auction by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDTO>> GetAuctionById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid auction ID.");
            }

            var auction = await _auctionService.GetAuctionByIdAsync(id);

            if (auction == null) return NotFound($"Auction with ID {id} not found.");

            return Ok(auction);
        }

        /// <summary>
        /// Retrieves all auctions.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<AuctionDTO>>> GetAllAuctions()
        {
            try
            {
                var auctions = await _auctionService.GetAllAuctionsAsync();

                return Ok(auctions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves auctions for a specific item by its ID.
        /// </summary>
        [HttpGet("item/{itemId}")]
        public async Task<ActionResult<List<AuctionDTO>>> GetAuctionsByItemId(int itemId)
        {
            try
            {
                var auctions = await _auctionService.GetAuctionsByItemIdAsync(itemId);

                if (auctions == null || auctions.Count == 0)
                {
                    return NotFound($"No auctions found for Item ID {itemId}.");
                }

                return Ok(auctions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing auction based on the provided auction data.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<AuctionDTO>> UpdateAuction(int id, [FromBody] AuctionDTO auctionDTO)
        {
            try
            {
                if (auctionDTO == null) return BadRequest("Auction data is required.");

                var updatedAuction = await _auctionService.UpdateAuctionAsync(id, auctionDTO);

                if (updatedAuction == null) return NotFound($"Auction with ID {id} not found.");

                return Ok(updatedAuction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of auctions associated with the specified manager ID.
        /// </summary>
        [HttpGet("manager/{managerId}")]
        public async Task<ActionResult<List<AuctionDTO>>> GetAuctionsByManagerId(int managerId)
        {
            try
            {
                var auctions = await _auctionService.GetAuctionsByManagerIdAsync(managerId);

                if (auctions == null || auctions.Count == 0)
                {
                    return NotFound($"No auctions found for Manager ID {managerId}.");
                }

                return Ok(auctions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the status of an auction and optionally sets the sold value.
        /// </summary>
        [HttpPut("status/{id}")]
        public async Task<ActionResult<AuctionDTO>> UpdateAuctionStatus(int id, [FromQuery] decimal soldValue)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Auction ID must be a positive number.");
                }

                if (soldValue < 0)
                {
                    return BadRequest("Sold value cannot be negative.");
                }

                var updatedAuction = await _auctionService.UpdateAuctionStatusAsync(id, soldValue);

                if (updatedAuction == null)
                {
                    return NotFound($"Auction with ID {id} not found.");
                }

                return Ok(updatedAuction);
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

        /// <summary>
        /// Deletes an auction by its ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Auction ID must be a positive number.");
                }

                var deleted = await _auctionService.DeleteAuctionAsync(id);

                if (!deleted)
                {
                    return NotFound($"Auction with ID {id} not found.");
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
