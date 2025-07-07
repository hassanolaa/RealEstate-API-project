using realstate.Services.DTOs;

namespace realstate.Services.Repositories.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> CreateAsync(CreateBookingDto dto);
        Task<BookingDto?> GetByIdAsync(int id);
        Task<IEnumerable<BookingDto>> GetByUserAsync(int userId, string? status = null);
        Task<IEnumerable<BookingDto>> GetByPropertyAsync(int propertyId, DateTime from, DateTime to);
        Task<IEnumerable<BookingDto>> GetUpcomingAsync(int days);
        Task UpdateStatusAsync(int bookingId, string newStatus);
        Task<bool> IsTimeSlotAvailableAsync(int propertyId, DateTime start, DateTime end);
        Task DeleteAsync(int id);
    }
}
