namespace realstate.Services.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = default!;
        public DateTime ProcessedAt { get; set; }
    }
}
