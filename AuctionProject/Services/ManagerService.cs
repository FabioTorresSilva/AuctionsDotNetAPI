using AuctionProject.Data;
using AuctionProject.Models;
using AuctionProject.Models.DTOs;
using AuctionProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionProject.Services
{
    public class ManagerService : IManagerService
    {
        private readonly AuctionContext _context;

        public ManagerService(AuctionContext context)
        {
            _context = context;
        }

        // Add a new manager
        public async Task<ManagerDTO> AddManagerAsync(ManagerDTO managerDTO)
        {
            if (string.IsNullOrWhiteSpace(managerDTO.Name))
            {
                throw new ArgumentException("Manager name cannot be empty.");
            }

            var manager = new Manager
            {
                Name = managerDTO.Name
            };

            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();

            return Manager.ManagerToDTO(manager);
        }

        // Get manager by ID
        public async Task<ManagerDTO?> GetManagerByIdAsync(int id)
        {
            var manager = await _context.Managers.FindAsync(id);
            return manager == null ? null : Manager.ManagerToDTO(manager);
        }

        // Get all managers
        public async Task<List<ManagerDTO>> GetAllManagersAsync()
        {
            var managers = await _context.Managers.ToListAsync();
            return managers.Select(Manager.ManagerToDTO).ToList();
        }

        // Update an existing manager
        public async Task<ManagerDTO?> UpdateManagerAsync(int id, ManagerDTO updatedManager)
        {
            var manager = await _context.Managers.FindAsync(id);

            if (manager == null) return null;

            manager.Name = updatedManager.Name;

            await _context.SaveChangesAsync();
            return Manager.ManagerToDTO(manager);
        }

        // Delete a manager
        public async Task<bool> DeleteManagerAsync(int id)
        {
            var manager = await _context.Managers.FindAsync(id);

            if (manager == null) return false;

            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
