namespace realstate.DAL.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Payment : BaseEntity
    {
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Booking")]
        public int? BookingId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(10)]
        public string Currency { get; set; } = "USD";

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty; // Stripe, PayPal, etc.

        [Required]
        [StringLength(100)]
        public string TransactionId { get; set; } = string.Empty;

        [StringLength(100)]
        public string? PaymentIntentId { get; set; } // For Stripe

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Completed, Failed, Refunded

        [Required]
        [StringLength(50)]
        public string PaymentType { get; set; } = string.Empty; // Booking Fee, Deposit, etc.

        [StringLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Fee { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? RefundAmount { get; set; }

        public DateTime? ProcessedAt { get; set; }

        public DateTime? RefundedAt { get; set; }

        [StringLength(1000)]
        public string? PaymentMetadata { get; set; } // JSON for additional payment data

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual Booking? Booking { get; set; }
    }

}
