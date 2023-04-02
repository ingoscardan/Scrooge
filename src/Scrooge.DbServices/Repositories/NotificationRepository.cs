using Microsoft.EntityFrameworkCore;
using Scrooge.DbServices.DBContext;
using Scrooge.DbServices.Entities;

namespace Scrooge.DbServices.Repositories;

public class NotificationRepository : DbRepository<NotificationEntity>, INotificationRepository
{
    public NotificationRepository(ScroogeDbContext context) : base(context)
    {
        Context.Set<NotificationEntity>();
    }

    public async Task<IEnumerable<NotificationEntity>?> GetUnread()
    {
        IEnumerable<NotificationEntity> allNotifications = await Context.Set<NotificationEntity>().ToListAsync();
        var notifications = allNotifications.Where(n => n.Status == NotificationStatus.Unread).ToList();
        return notifications.Any() ? notifications : null;
    }

    public async Task<IEnumerable<NotificationEntity>?> GetRead()
    {
        IEnumerable<NotificationEntity> allNotifications = await Context.Set<NotificationEntity>().ToListAsync();
        var notifications = allNotifications.Where(n => n.Status == NotificationStatus.Read).ToList();
        return notifications.Any() ? notifications : null;
    }

    public async Task<IEnumerable<NotificationEntity>?> GetTop(int top = 10)
    {
        IEnumerable<NotificationEntity> allNotifications = await Context.Set<NotificationEntity>().ToListAsync();
        var notifications = allNotifications.OrderBy(n => n.NotificationId).Take(top).ToList();
        return notifications.Any() ? notifications : null;
    }
}