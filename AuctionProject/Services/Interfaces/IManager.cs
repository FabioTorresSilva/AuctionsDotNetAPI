using AuctionProject.Models.DTOs;

namespace AuctionProject.Services.Interfaces
{
    /// <summary>
    /// Service interface for managing managers.
    /// </summary>
    public interface IManagerService
    {
        /// <summary>
        /// Adds a new manager.
        /// </summary>
        Task<ManagerDTO> AddManagerAsync(ManagerDTO managerDTO);

        /// <summary>
        /// Retrieves a manager by its ID.
        /// </summary>
        Task<ManagerDTO?> GetManagerByIdAsync(int id);

        /// <summary>
        /// Retrieves all managers.
        /// </summary>
        Task<List<ManagerDTO>> GetAllManagersAsync();

        /// <summary>
        /// Updates an existing manager.
        /// </summary>
        Task<ManagerDTO?> UpdateManagerAsync(int id, ManagerDTO updatedManager);

        /// <summary>
        /// Deletes a manager by its ID.
        /// </summary>
        Task<bool> DeleteManagerAsync(int id);
    }
}
