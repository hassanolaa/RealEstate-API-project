using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using realstate.Services.DTOs;
using realstate.Services.Repositories.Interfaces;

namespace realstate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _service.GetByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpGet("transaction/{txId}")]
        public async Task<IActionResult> GetByTransaction(string txId)
        {
            var payment = await _service.GetByTransactionIdAsync(txId);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
            => Ok(await _service.GetByUserAsync(userId));

        [HttpPost]
        [EnableRateLimiting("CreatePolicy")]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
        {
            var payment = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
        }

        [HttpPost("{id}/refund")]
        public async Task<IActionResult> Refund(int id, [FromBody] RefundDto dto)
        {
            await _service.RefundAsync(id, dto.Amount);
            return NoContent();
        }
    }
}
