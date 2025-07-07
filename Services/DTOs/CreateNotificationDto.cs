namespace realstate.Services.DTOs
{
    public class CreateNotificationDto
    {
        public int UserId { get; set; }
        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string Type { get; set; } = default!;       // Email, SMS, Push, InApp
        public string Category { get; set; } = default!;   // Booking, Payment, Property
    }
}
