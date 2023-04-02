using Scrooge.DbServices.Repositories;

namespace Scrooge.DbServices;

public interface IDbUnitOfWork<TEntity> : IDisposable where TEntity: class
{
    void Save();
    IDbRepository<TEntity> Repository { get; set; }
    INotificationRepository NotificationRepository { get; set; }
}