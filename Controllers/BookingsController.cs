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
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingsController(IBookingService service)
        {
            _service = service;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId, string? status = null)
            => Ok(await _service.GetByUserAsync(userId, status));

        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetByProperty(int propertyId, DateTime from, DateTime to)
            => Ok(await _service.GetByPropertyAsync(propertyId, from, to));

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming(int days = 7)
            => Ok(await _service.GetUpcomingAsync(days));

        [HttpPost]
        [EnableRateLimiting("CreatePolicy")]
        public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
        {
            var booking = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByUser), new { userId = booking.UserId }, booking);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateBookingStatusDto dto)
        {
            await _service.UpdateStatusAsync(id, dto.NewStatus);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
