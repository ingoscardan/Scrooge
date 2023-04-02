using Scrooge.DbServices.Entities;

namespace Scrooge.DbServices.Repositories;

public interface INotificationRepository : IDbRepository<NotificationEntity>
{
    Task<IEnumerable<NotificationEntity>?> GetUnread();
    Task<IEnumerable<NotificationEntity>?> GetRead();
    Task<IEnumerable<NotificationEntity>?> GetTop(int top = 10);
}