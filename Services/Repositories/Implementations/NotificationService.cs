using AutoMapper;
using realstate.DAL.Models;
using realstate.DAL.Repositories.Interfaces;
using realstate.Services.DTOs;
using realstate.Services.Repositories.Interfaces;

namespace realstate.Services.Repositories.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<NotificationDto> SendAsync(CreateNotificationDto dto)
        {
            var entity = _mapper.Map<Notification>(dto);
            await _uow.Notifications.AddAsync(entity);
            await _uow.SaveChangesAsync();
            // dispatch via email/SMS/push here
            return _mapper.Map<NotificationDto>(entity);
        }

        public async Task<IEnumerable<NotificationDto>> GetByUserAsync(int userId, bool onlyUnread = false)
        {
            var list = onlyUnread
                ? await _uow.Notifications.GetUnreadNotificationsAsync(userId)
                : await _uow.Notifications.GetByIdAsync(userId) as IEnumerable<Notification>;
            return _mapper.Map<IEnumerable<NotificationDto>>(list);
        }

        public async Task<IEnumerable<NotificationDto>> GetPendingAsync()
        {
            var list = await _uow.Notifications.GetPendingNotificationsAsync();
            return _mapper.Map<IEnumerable<NotificationDto>>(list);
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            await _uow.Notifications.MarkAllAsReadAsync(userId);
            await _uow.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            await _uow.Notifications.MarkAsReadAsync(notificationId);
            await _uow.SaveChangesAsync();
        }
    }
}
