namespace realstate.Services.DTOs
{
    public class CreatePropertyDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
        public string Country { get; set; } = "United States";
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal SquareFootage { get; set; }
        public bool IsFeatured { get; set; }
    }
}
