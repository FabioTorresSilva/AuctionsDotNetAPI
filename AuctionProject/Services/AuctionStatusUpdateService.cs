using AuctionProject.Data;
using AuctionProject.Models;
using AuctionProject.Models.Enums;
using AuctionProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class AuctionStatusUpdateService : BackgroundService, IAuctionStatusUpdateService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AuctionStatusUpdateService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromHours(24);

    public AuctionStatusUpdateService(IServiceProvider serviceProvider, ILogger<AuctionStatusUpdateService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    /// Updates the statuses of all auctions based on their current status and dates.
    /// This method checks auctions' end dates and updates their status accordingly.
    /// </summary>
    public async Task UpdateAuctionStatusesAsync()
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AuctionContext>();
                await UpdateAuctionStatuses(context);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating auction statuses: {ex.Message}");
        }
    }

    /// <summary>
    /// Executes the auction status update service at regular intervals.
    /// This method runs continuously until the service is stopped, calling the method that updates auction statuses at each interval.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Auction Status Update Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Call the method that implements the auction status update logic
                await UpdateAuctionStatusesAsync();

                // Log that the auction statuses were updated successfully
                _logger.LogInformation("Auction statuses updated successfully.");

                // Wait before running again, with the cancellation token passed to handle service shutdown gracefully
                await Task.Delay(_interval, stoppingToken);
            }
            catch (Exception ex)
            {
                // Log any exceptions encountered during the execution of the auction status update process
                _logger.LogError($"Error occurred while updating auction statuses: {ex.Message}", ex);
            }
        }

        _logger.LogInformation("Auction Status Update Service is stopping.");
    }

    /// <summary>
    /// Checks and updates the status of auctions based on the current date.
    /// Auctions are opened if the start date has passed, and auctions are closed or marked as sold if the end date has passed.
    /// </summary>
    private async Task UpdateAuctionStatuses(AuctionContext context)
    {
        _logger.LogInformation("Checking and updating auction statuses...");

        var today = DateTime.UtcNow.Date;
        _logger.LogInformation($"Today: {today}");

        var auctions = await context.Auctions
            .ToListAsync(); 

        _logger.LogInformation($"Found {auctions.Count} auctions to process.");

        foreach (var auction in auctions)
        {
            _logger.LogInformation($"Auction {auction.Id}: Status = {auction.Status}, StartDate = {auction.StartDate.Date}, EndDate = {auction.EndDate.Date}");

            if (today < auction.StartDate.Date)
            {
                auction.Status = AuctionStatus.Pending;
                _logger.LogInformation($"Auction {auction.Id} status changed to Pending (StartDate is in the future).");
            }
            else if (today >= auction.StartDate.Date && today <= auction.EndDate.Date)
            {
                auction.Status = AuctionStatus.Open;
                _logger.LogInformation($"Auction {auction.Id} status changed to Open (Today is between StartDate and EndDate).");
            }
            else if (today > auction.EndDate.Date)
            {
                if (auction.SoldValue >= auction.StartingPrice)
                {
                    auction.Status = AuctionStatus.Sold;
                    _logger.LogInformation($"Auction {auction.Id} status changed to Sold (SoldValue >= StartingPrice).");
                }
                else
                {
                    auction.Status = AuctionStatus.Close;
                    _logger.LogInformation($"Auction {auction.Id} status changed to Close (SoldValue < StartingPrice).");
                }
            }

            var item = await context.Items.FirstOrDefaultAsync(i => i.Id == auction.ItemId);
            if (item != null)
            {
                item.Status = auction.Status == AuctionStatus.Sold ? ItemStatus.Sold : ItemStatus.Available;
                _logger.LogInformation($"Item {item.Id} status updated to {item.Status}");
            }
        }

        try
        {
            await context.SaveChangesAsync();
            _logger.LogInformation("Auction statuses saved to the database.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error saving auction statuses: {ex.Message}");
        }
    }
}