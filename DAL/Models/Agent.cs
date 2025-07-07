namespace realstate.DAL.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Agent : BaseEntity
    {
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string LicenseNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Company { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Bio { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal? Rating { get; set; }

        public int ReviewCount { get; set; } = 0;

        [Required]
        public bool IsVerified { get; set; } = false;

        [Column(TypeName = "decimal(5,2)")]
        public decimal CommissionRate { get; set; } = 3.0m;

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
    }

}
