namespace AuctionProject.Models.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public string? ManagerId { get; set; }

        public ItemDTO() { }

        public ItemDTO(int id, string name, string url, string? description, List<int> categoryIds, string? managerId)
        {
            Id = id;
            Name = name;
            Url = url;
            Description = description;
            CategoryIds = categoryIds;
            ManagerId = managerId;
        }

        public ItemDTO(string name, string url, string? description, List<int> categoryIds, string? managerId)
        {
            Name = name;
            Url = url;
            Description = description;
            CategoryIds = categoryIds;
            ManagerId = managerId;
        }
    }
}
