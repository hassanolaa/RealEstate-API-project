using AutoMapper;
using realstate.DAL.Models;
using realstate.DAL.Repositories.Interfaces;
using realstate.Services.DTOs;
using realstate.Services.Repositories.Interfaces;

namespace realstate.Services.Repositories.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PaymentDto> CreateAsync(CreatePaymentDto dto)
        {
            var entity = _mapper.Map<Payment>(dto);
            await _uow.Payments.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<PaymentDto>(entity);
        }

        public async Task RefundAsync(int paymentId, decimal amount)
        {
            var payment = await _uow.Payments.GetByIdAsync(paymentId);
            if (payment == null) throw new KeyNotFoundException("Payment not found");
            payment.RefundAmount = amount;
            payment.Status = "Refunded";
            await _uow.Payments.UpdateAsync(payment);
            await _uow.SaveChangesAsync();
        }

        public async Task<PaymentDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.Payments.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<PaymentDto>(entity);
        }

        public async Task<PaymentDto?> GetByTransactionIdAsync(string transactionId)
        {
            var entity = await _uow.Payments.GetPaymentByTransactionIdAsync(transactionId);
            return entity == null ? null : _mapper.Map<PaymentDto>(entity);
        }

        public async Task<IEnumerable<PaymentDto>> GetByBookingAsync(int bookingId)
        {
            var list = await _uow.Payments.GetPaymentsByBookingAsync(bookingId);
            return _mapper.Map<IEnumerable<PaymentDto>>(list);
        }

        public async Task<IEnumerable<PaymentDto>> GetByUserAsync(int userId)
        {
            var list = await _uow.Payments.GetPaymentsByUserAsync(userId);
            return _mapper.Map<IEnumerable<PaymentDto>>(list);
        }

        public async Task<IEnumerable<PaymentDto>> GetFailedAsync()
        {
            var list = await _uow.Payments.GetFailedPaymentsAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(list);
        }

        public async Task<decimal> GetTotalByPeriodAsync(DateTime from, DateTime to)
            => await _uow.Payments.GetTotalPaymentsByPeriodAsync(from, to);
    }
}
