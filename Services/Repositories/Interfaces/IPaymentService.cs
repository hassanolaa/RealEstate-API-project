using realstate.Services.DTOs;

namespace realstate.Services.Repositories.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDto> CreateAsync(CreatePaymentDto dto);
        Task<PaymentDto?> GetByIdAsync(int id);
        Task<PaymentDto?> GetByTransactionIdAsync(string transactionId);
        Task<IEnumerable<PaymentDto>> GetByUserAsync(int userId);
        Task<IEnumerable<PaymentDto>> GetByBookingAsync(int bookingId);
        Task<decimal> GetTotalByPeriodAsync(DateTime from, DateTime to);
        Task<IEnumerable<PaymentDto>> GetFailedAsync();
        Task RefundAsync(int paymentId, decimal amount);
    }
}
