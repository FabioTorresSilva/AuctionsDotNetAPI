using AuctionProject.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace AuctionProject.Models
{
    /// <summary>
    /// Represents a manager in the auction system.
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// Gets or sets the unique identifier for the manager.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the manager.
        /// The name is required and has a maximum length of 50 characters.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Converts a Manager object to a ManagerDTO.
        /// </summary>
        /// <param name="manager">The manager object to be converted.</param>
        /// <returns>A ManagerDTO containing the Id and Name of the manager.</returns>
        public static ManagerDTO ManagerToDTO(Manager manager)
        {
            return new ManagerDTO
            {
                Id = manager.Id,
                Name = manager.Name
            };
        }

        /// <summary>
        /// Converts a collection of Manager objects to a list of ManagerDTOs.
        /// </summary>
        /// <param name="managers">The collection of managers to be converted.</param>
        /// <returns>A list of ManagerDTO objects representing the managers.</returns>
        public static List<ManagerDTO> ManagerListToDTO(IEnumerable<Manager> managers)
        {
            return managers.Select(ManagerToDTO).ToList();
        }
    }
}
