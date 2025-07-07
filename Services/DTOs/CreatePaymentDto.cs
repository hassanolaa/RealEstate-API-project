namespace realstate.Services.DTOs
{
    public class CreatePaymentDto
    {
        public int UserId { get; set; }
        public int? BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string PaymentMethod { get; set; } = default!;
        public string PaymentType { get; set; } = default!;
    }
}
