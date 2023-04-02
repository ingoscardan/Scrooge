using AutoMapper;
using Scrooge.DbServices;
using Scrooge.DbServices.Entities;
using Scrooge.Services.Models;

namespace Scrooge.Services.Services;

public interface INotificationService : IService<NotificationModel, NotificationEntity>
{
    Task<IList<NotificationModel>> GetUnreadNotifications();
    Task<IList<NotificationModel>> GetReadNotifications();
}