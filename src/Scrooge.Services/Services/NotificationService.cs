using AutoMapper;
using Scrooge.DbServices;
using Scrooge.DbServices.Entities;
using Scrooge.Services.Models;

namespace Scrooge.Services.Services;

public class NotificationService : Service<NotificationModel, NotificationEntity>, INotificationService
{
    public NotificationService(IDbUnitOfWork<NotificationEntity> unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<IList<NotificationModel>> GetUnreadNotifications()
    {
        var entity = await DbUnitOfWork.NotificationRepository.GetUnread();
        return Mapper.Map<IList<NotificationModel>>(entity);
    }

    public async Task<IList<NotificationModel>> GetReadNotifications()
    {
        var entity = await DbUnitOfWork.NotificationRepository.GetRead();
        return Mapper.Map<IList<NotificationModel>>(entity);
    }
}