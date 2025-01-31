using AuctionProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuctionStatusUpdateController : ControllerBase
{
    private readonly IAuctionStatusUpdateService _auctionStatusUpdateService;

    public AuctionStatusUpdateController(IAuctionStatusUpdateService auctionStatusUpdateService)
    {
        _auctionStatusUpdateService = auctionStatusUpdateService;
    }

    /// <summary>
    /// Updates the status of all auctions based on their start and end dates.
    /// </summary>
    [HttpPost("update-status")]
    public async Task<IActionResult> UpdateStatus()
    {
        try
        {
            await _auctionStatusUpdateService.UpdateAuctionStatusesAsync();
            return Ok("Auction status updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating auction statuses.");
        }
    }
}