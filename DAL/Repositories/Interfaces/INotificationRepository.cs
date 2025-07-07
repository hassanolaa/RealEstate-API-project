using realstate.DAL.Models;

namespace realstate.DAL.Repositories.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int userId);
        Task<IEnumerable<Notification>> GetNotificationsByTypeAsync(int userId, string type);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(int userId);
        Task<IEnumerable<Notification>> GetPendingNotificationsAsync();
    }
}
