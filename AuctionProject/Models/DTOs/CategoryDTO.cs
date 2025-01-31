namespace AuctionProject.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for transferring category data.
    /// </summary>
    public class CategoryDTO
    {
        /// <summary>
        /// The unique identifier for the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Name { get; set; }
    }
}
