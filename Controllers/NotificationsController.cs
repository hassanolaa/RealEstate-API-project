using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using realstate.Services.DTOs;
using realstate.Services.Repositories.Interfaces;

namespace realstate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationsController(INotificationService service)
        {
            _service = service;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId, bool onlyUnread = false)
            => Ok(await _service.GetByUserAsync(userId, onlyUnread));

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] CreateNotificationDto dto)
        {
            var notification = await _service.SendAsync(dto);
            return CreatedAtAction(nameof(GetByUser), new { userId = notification.UserId }, notification);
        }

        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _service.MarkAsReadAsync(id);
            return NoContent();
        }

        [HttpPatch("user/{userId}/read-all")]
        public async Task<IActionResult> MarkAllAsRead(int userId)
        {
            await _service.MarkAllAsReadAsync(userId);
            return NoContent();
        }
    }
}
