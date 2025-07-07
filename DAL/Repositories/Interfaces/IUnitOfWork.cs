namespace realstate.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IPropertyRepository Properties { get; }
        IBookingRepository Bookings { get; }
        IPaymentRepository Payments { get; }
        INotificationRepository Notifications { get; }
        Task<int> SaveChangesAsync();
    }
}
    