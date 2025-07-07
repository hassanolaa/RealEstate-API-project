using realstate.DAL.Models;

namespace realstate.DAL.Repositories.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId);
        Task<IEnumerable<Payment>> GetPaymentsByBookingAsync(int bookingId);
        Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId);
        Task<decimal> GetTotalPaymentsByPeriodAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<Payment>> GetFailedPaymentsAsync();
    }
}
