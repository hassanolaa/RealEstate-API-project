namespace realstate.DAL.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Booking : BaseEntity
    {
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        [Required]
        [StringLength(50)]
        public string BookingType { get; set; } = "Viewing"; // Viewing, Inspection, Meeting

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public TimeSpan ScheduledTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Scheduled"; // Scheduled, Confirmed, Completed, Cancelled

        [StringLength(1000)]
        public string? Notes { get; set; }

        [StringLength(500)]
        public string? ContactMessage { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? BookingFee { get; set; }

        public DateTime? ConfirmedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public DateTime? CancelledAt { get; set; }

        [StringLength(500)]
        public string? CancellationReason { get; set; }

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual Property Property { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}   
