namespace realstate.Services.DTOs
{
    public class CreateBookingDto
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan ScheduledTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
