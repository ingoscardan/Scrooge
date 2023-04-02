using Microsoft.EntityFrameworkCore;
using Scrooge.DbServices.DBContext;
using Scrooge.DbServices.Repositories;

namespace Scrooge.DbServices;

public sealed class DbUnitOfWork<TEntity> : IDbUnitOfWork<TEntity> where TEntity : class
{
    private readonly ScroogeDbContext _dbContext;
    public IDbRepository<TEntity> Repository { get; set; }
    public INotificationRepository NotificationRepository { get; set; }

    public DbUnitOfWork(
        ScroogeDbContext dbContext,
        IDbRepository<TEntity> repository,
        INotificationRepository notificationRepository)
    {
        _dbContext = dbContext;
        Repository = repository;
        NotificationRepository = notificationRepository;
    }
    public void Dispose()
    {
        _dbContext.Dispose();
    }
    public void Save()
    {
        _dbContext.SaveChanges();
    }
}