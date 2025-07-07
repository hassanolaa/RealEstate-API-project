using Microsoft.EntityFrameworkCore;
using realstate.DAL.Models;
using realstate.DAL.Repositories.Interfaces;

namespace realstate.DAL.Repositories.Implementations
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int userId)
        {
            return await _context.Notifications
                .AsNoTracking()
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByTypeAsync(int userId, string type)
        {
            return await _context.Notifications
                .AsNoTracking()
                .Where(n => n.UserId == userId && n.Type == type)
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                _context.Notifications.Update(notification);
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            var list = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();
            foreach (var n in list)
                n.IsRead = true;
            _context.Notifications.UpdateRange(list);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetPendingNotificationsAsync()
        {
            return await _context.Notifications
                .AsNoTracking()
                .Where(n => !n.IsSent)
                .ToListAsync();
        }
    }
}
