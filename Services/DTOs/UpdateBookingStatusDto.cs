namespace realstate.Services.DTOs
{
    public class UpdateBookingStatusDto
    {
        public string NewStatus { get; set; } = default!; // Scheduled, Confirmed, Completed, Cancelled
    }
}
