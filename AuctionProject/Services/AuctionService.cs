using AuctionProject.Data;
using AuctionProject.Models;
using AuctionProject.Models.DTOs;
using AuctionProject.Models.Enums;
using AuctionProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionProject.Services
{
    public class AuctionService : IAuctionService

    {
        private readonly AuctionContext _context;

        public AuctionService(AuctionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new auction based on the provided AuctionDTO.
        /// </summary>
        public async Task<AuctionDTO> CreateAuctionAsync(AuctionDTO auctionDTO)
        {
            if (auctionDTO == null)
            {
                throw new ArgumentException("Auction data is required.");
            }

            var item = await _context.Items.FindAsync(auctionDTO.ItemId);

            if (item == null)
            {
                throw new ArgumentException("Invalid Item ID provided.");
            }

            var existingAuction = await _context.Auctions
                .Where(a => a.ItemId == auctionDTO.ItemId &&
                    (a.Status == AuctionStatus.Pending || a.Status == AuctionStatus.Open))
                .FirstOrDefaultAsync();

            if (auctionDTO.StartDate < DateTime.Now)
            {
                throw new ArgumentException("Start date cannot be in the past.");
            }

            if (auctionDTO.StartDate >= auctionDTO.EndDate)
            {
                throw new ArgumentException("Start date must be before end date.");
            }

            var manager = await _context.Managers.FindAsync(auctionDTO.ManagerId);
            if (manager == null)
            {
                throw new ArgumentException("Invalid Manager ID provided.");
            }

            var auction = new Auction
            {
                ItemId = auctionDTO.ItemId,
                AuctionType = auctionDTO.AuctionType,
                StartingPrice = auctionDTO.StartingPrice,
                StartDate = auctionDTO.StartDate,
                EndDate = auctionDTO.EndDate,
                Status = AuctionStatus.Pending,
                ManagerId = auctionDTO.ManagerId,
                SoldValue = 0
            };

            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();

            return Auction.AuctionToDTO(auction);
        }

        /// <summary>
        /// Retrieves an auction by its unique identifier, including related item details.
        /// </summary>
        public async Task<AuctionDTO?> GetAuctionByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid auction ID provided.");
            }

            var auction = await _context.Auctions
                .Include(a => a.Item)
                .FirstOrDefaultAsync(a => a.Id == id);

            return auction == null ? null : Auction.AuctionToDTO(auction);
        }

        /// <summary>
        /// Retrieves all auctions, including related item details, and maps them to AuctionDTOs.
        /// </summary>
        public async Task<List<AuctionDTO>> GetAllAuctionsAsync()
        {
            try
            {
                var auctions = await _context.Auctions
                    .Include(a => a.Item)
                    .ToListAsync();

                if (auctions == null || !auctions.Any())
                {
                    return new List<AuctionDTO>();
                }

                var auctionDTOs = auctions.Select(Auction.AuctionToDTO).ToList();

                return auctionDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving auctions.", ex);
            }
        }

        /// <summary>
        /// Retrieves all auctions associated with a specific item by its ItemId, including related item details.
        /// </summary>
        public async Task<List<AuctionDTO>> GetAuctionsByItemIdAsync(int itemId)
        {
            if (itemId <= 0)
            {
                throw new ArgumentException("Invalid Item ID provided.", nameof(itemId));
            }

            try
            {

                var auctions = await _context.Auctions
                    .Where(a => a.ItemId == itemId)
                    .Include(a => a.Item)
                    .ToListAsync();

                if (auctions == null || !auctions.Any())
                {
                    return new List<AuctionDTO>();
                }

                var auctionDTOs = auctions.Select(Auction.AuctionToDTO).ToList();

                return auctionDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving auctions for the specified ItemId.", ex);
            }
        }

        /// <summary>
        /// Updates an existing auction based on its ID with the provided updated details.
        /// </summary>
        public async Task<AuctionDTO?> UpdateAuctionAsync(int id, AuctionDTO updatedAuctionDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid auction ID provided.", nameof(id));
            }

            try
            {
                var auction = await _context.Auctions
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (auction == null) return null;

                var item = await _context.Items.FindAsync(updatedAuctionDTO.ItemId);

                if (item == null)
                {
                    throw new ArgumentException("Invalid Item ID provided.");
                }

                if (updatedAuctionDTO.StartDate < DateTime.Now)
                {
                    throw new ArgumentException("Start date cannot be in the past.");
                }

                if (updatedAuctionDTO.StartDate >= updatedAuctionDTO.EndDate)
                {
                    throw new ArgumentException("Start date must be before end date.");
                }

                auction.AuctionType = updatedAuctionDTO.AuctionType;
                auction.StartingPrice = updatedAuctionDTO.StartingPrice;
                auction.StartDate = updatedAuctionDTO.StartDate;
                auction.EndDate = updatedAuctionDTO.EndDate;
                auction.ManagerId = updatedAuctionDTO.ManagerId;

                await _context.SaveChangesAsync();

                return Auction.AuctionToDTO(auction);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error occurred during auction update: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the auction.", ex);
            }
        }

        //Not needed
        public async Task<AuctionDTO?> UpdateAuctionStatusAsync(int id, decimal soldValue)
        {
            var auction = await _context.Auctions
                .FirstOrDefaultAsync(a => a.Id == id);

            if (auction == null) return null;

            if (auction.EndDate > DateTime.Now)
            {
                throw new ArgumentException("Auction cannot be marked as sold before the end date.");
            }

            if (soldValue < auction.StartingPrice)
            {
                throw new ArgumentException("Sold value must be greater than or equal to the starting price.");
            }

            auction.Status = AuctionStatus.Sold;
            auction.SoldValue = soldValue;

            await _context.SaveChangesAsync();

            return Auction.AuctionToDTO(auction);
        }

        /// <summary>
        /// Retrieves all auctions related to a specific manager's items.
        /// </summary>
        public async Task<List<AuctionDTO>> GetAuctionsByManagerIdAsync(int managerId)
        {
            if (managerId <= 0)
            {
                throw new ArgumentException("Invalid manager ID.");
            }
            try
            {
                var auctions = await _context.Auctions
                    .Include(a => a.Item)
                    .Where(a => a.Item.ManagerId == managerId)
                    .ToListAsync();

                if (!auctions.Any())
                {
                    throw new InvalidOperationException($"No auctions found for manager with ID {managerId}.");
                }

                return auctions.Select(Auction.AuctionToDTO).ToList();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error while retrieving auctions: " + ex.Message, ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Error while retrieving auctions: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while retrieving auctions.", ex);
            }
        }

        /// <summary>
        /// Deletes Auction by id
        /// </summary>
        public async Task<bool> DeleteAuctionAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid auction ID. It must be greater than zero.", nameof(id));
            }

            try
            {
                var auction = await _context.Auctions.FindAsync(id);

                if (auction == null)
                {
                    return false;
                }

                _context.Auctions.Remove(auction);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Error while deleting auction: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the auction.", ex);
            }
        }

        public async Task<List<AuctionDTO>> GetOpenAuctions()
        {
            var openAuctions = await _context.Auctions
                .Where(a => a.Status == AuctionStatus.Open)
                .ToListAsync();

            return openAuctions.Select(Auction.AuctionToDTO).ToList();
        }
    }
}