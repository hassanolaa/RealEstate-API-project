namespace realstate.Services.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public int ViewCount { get; set; }
    }
}
