namespace realstate.DAL.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Notification : BaseEntity
    {
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty; // Email, SMS, Push, In-App

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty; // Booking, Payment, Property, etc.

        [Required]
        public bool IsRead { get; set; } = false;

        [Required]
        public bool IsSent { get; set; } = false;

        public DateTime? SentAt { get; set; }

        public DateTime? ReadAt { get; set; }

        [StringLength(500)]
        public string? ActionUrl { get; set; }

        [StringLength(1000)]
        public string? Metadata { get; set; } // JSON for additional notification data

        // Navigation Properties
        public virtual User User { get; set; } = null!;
    }

}
