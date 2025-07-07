using AutoMapper;
using realstate.DAL.Models;
using realstate.DAL.Repositories.Interfaces;
using realstate.Services.DTOs;
using realstate.Services.Repositories.Interfaces;

namespace realstate.Services.Repositories.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<BookingDto> CreateAsync(CreateBookingDto dto)
        {
            if (!await _uow.Bookings.IsTimeSlotAvailableAsync(dto.PropertyId, dto.ScheduledDate + dto.ScheduledTime, dto.ScheduledDate + dto.ScheduledTime.Add(dto.Duration)))
            {
                throw new InvalidOperationException("Time slot unavailable");
            }
            var entity = _mapper.Map<Booking>(dto);
            await _uow.Bookings.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<BookingDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _uow.Bookings.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<bool> IsTimeSlotAvailableAsync(int propertyId, DateTime start, DateTime end)
            => await _uow.Bookings.IsTimeSlotAvailableAsync(propertyId, start, end);

        public async Task<IEnumerable<BookingDto>> GetByPropertyAsync(int propertyId, DateTime from, DateTime to)
        {
            var list = await _uow.Bookings.GetBookingsByPropertyAsync(propertyId, from, to);
            return _mapper.Map<IEnumerable<BookingDto>>(list);
        }

        public async Task<IEnumerable<BookingDto>> GetByUserAsync(int userId, string? status = null)
        {
            var list = await _uow.Bookings.GetBookingsByUserAsync(userId, status);
            return _mapper.Map<IEnumerable<BookingDto>>(list);
        }

        public async Task<BookingDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.Bookings.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<BookingDto>(entity);
        }

        public async Task<IEnumerable<BookingDto>> GetUpcomingAsync(int days)
        {
            var list = await _uow.Bookings.GetUpcomingBookingsAsync(days);
            return _mapper.Map<IEnumerable<BookingDto>>(list);
        }

        public async Task UpdateStatusAsync(int bookingId, string newStatus)
        {
            await _uow.Bookings.UpdateBookingStatusAsync(bookingId, newStatus);
            await _uow.SaveChangesAsync();
        }
    }
}
