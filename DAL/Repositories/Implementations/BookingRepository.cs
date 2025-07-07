using Microsoft.EntityFrameworkCore;
using realstate.DAL.Models;
using realstate.DAL.Repositories.Interfaces;

namespace realstate.DAL.Repositories.Implementations
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context) { }

        public async Task<bool> IsTimeSlotAvailableAsync(int propertyId, DateTime startTime, DateTime endTime)
        {
            return !await _context.Bookings
                .AnyAsync(b =>
                    b.PropertyId == propertyId &&
                    ((b.ScheduledDate + b.ScheduledTime) < endTime && (b.ScheduledDate + b.ScheduledTime) > startTime));
        }

        public async Task<IEnumerable<Booking>> GetBookingsByPropertyAsync(int propertyId, DateTime fromDate, DateTime toDate)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(b =>
                    b.PropertyId == propertyId &&
                    b.ScheduledDate >= fromDate.Date &&
                    b.ScheduledDate <= toDate.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserAsync(int userId, string? status = null)
        {
            var query = _context.Bookings.AsNoTracking().Where(b => b.UserId == userId);
            if (!string.IsNullOrEmpty(status))
                query = query.Where(b => b.Status == status);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetUpcomingBookingsAsync(int days = 7)
        {
            var today = DateTime.UtcNow.Date;
            var endDate = today.AddDays(days);
            return await _context.Bookings
                .AsNoTracking()
                .Where(b => b.ScheduledDate >= today && b.ScheduledDate <= endDate)
                .ToListAsync();
        }

        public async Task UpdateBookingStatusAsync(int bookingId, string newStatus)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                booking.Status = newStatus;
                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}
