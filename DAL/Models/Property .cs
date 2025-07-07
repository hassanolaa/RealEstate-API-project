using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace realstate.DAL.Models
{
    public class Property: BaseEntity
    {
     [Required]
     [StringLength(200)]
     public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = "United States";

        [Column(TypeName = "decimal(10,8)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(11,8)")]
        public decimal? Longitude { get; set; }

        [Required]
        [StringLength(50)]
        public string PropertyType { get; set; } = string.Empty; // House, Apartment, Condo, etc.

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Available"; // Available, Sold, Rented, Pending

        [Required]
        public int Bedrooms { get; set; }

        [Required]
        public int Bathrooms { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal SquareFootage { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal? LotSize { get; set; }

        public int? YearBuilt { get; set; }

        public int? ParkingSpaces { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public bool IsFeatured { get; set; } = false;

        public int ViewCount { get; set; } = 0;

        [StringLength(1000)]
        public string? Features { get; set; } // JSON string for additional features

        [ForeignKey("Agent")]
        public int? AgentId { get; set; }

        public DateTime? ListedAt { get; set; }

        // Navigation Properties

        public virtual Agent? Agent { get; set; }

        public virtual ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();


        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
