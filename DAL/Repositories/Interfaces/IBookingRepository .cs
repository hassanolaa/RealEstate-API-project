using realstate.DAL.Models;

namespace realstate.DAL.Repositories.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<bool> IsTimeSlotAvailableAsync(int propertyId, DateTime startTime, DateTime endTime);
        Task<IEnumerable<Booking>> GetBookingsByPropertyAsync(int propertyId, DateTime fromDate, DateTime toDate);
        Task<IEnumerable<Booking>> GetBookingsByUserAsync(int userId, string? status = null);
        Task<IEnumerable<Booking>> GetUpcomingBookingsAsync(int days = 7);
        Task UpdateBookingStatusAsync(int bookingId, string newStatus);
    }
}
