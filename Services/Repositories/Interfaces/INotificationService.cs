using realstate.Services.DTOs;

namespace realstate.Services.Repositories.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationDto> SendAsync(CreateNotificationDto dto);
        Task<IEnumerable<NotificationDto>> GetByUserAsync(int userId, bool onlyUnread = false);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(int userId);
        Task<IEnumerable<NotificationDto>> GetPendingAsync();
    }
}
