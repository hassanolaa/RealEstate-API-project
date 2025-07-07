using realstate.DAL.Repositories.Interfaces;

namespace realstate.DAL.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; }
        public IPropertyRepository Properties { get; }
        public IBookingRepository Bookings { get; }
        public IPaymentRepository Payments { get; }
        public INotificationRepository Notifications { get; }

        public UnitOfWork(
            AppDbContext context,
            IUserRepository users,
            IPropertyRepository properties,
            IBookingRepository bookings,
            IPaymentRepository payments,
            INotificationRepository notifications)
        {
            _context = context;
            Users = users;
            Properties = properties;
            Bookings = bookings;
            Payments = payments;
            Notifications = notifications;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
