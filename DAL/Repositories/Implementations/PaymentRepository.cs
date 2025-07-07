using Microsoft.EntityFrameworkCore;
using realstate.DAL.Models;
using realstate.DAL.Repositories.Interfaces;

namespace realstate.DAL.Repositories.Implementations
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId)
        {
            return await _context.Payments
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByBookingAsync(int bookingId)
        {
            return await _context.Payments
                .AsNoTracking()
                .Where(p => p.BookingId == bookingId)
                .ToListAsync();
        }

        public async Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId)
        {
            return await _context.Payments
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.TransactionId == transactionId);
        }

        public async Task<decimal> GetTotalPaymentsByPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            return await _context.Payments
                .AsNoTracking()
                .Where(p => p.ProcessedAt >= fromDate && p.ProcessedAt <= toDate)
                .SumAsync(p => p.Amount);
        }

        public async Task<IEnumerable<Payment>> GetFailedPaymentsAsync()
        {
            return await _context.Payments
                .AsNoTracking()
                .Where(p => p.Status == "Failed")
                .ToListAsync();
        }
    }
}
