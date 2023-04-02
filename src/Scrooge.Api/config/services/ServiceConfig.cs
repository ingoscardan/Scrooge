using Scrooge.DbServices;
using Scrooge.DbServices.Entities;
using Scrooge.DbServices.Repositories;

namespace Scrooge.Api.config.services;

public static class ServiceConfig
{
    public static IServiceCollection ConfigureDbService(this IServiceCollection services)
    {
        services.AddScoped<IDbUnitOfWork<NotificationEntity>, DbUnitOfWork<NotificationEntity>>();
        services.AddScoped<IDbRepository<NotificationEntity>, DbRepository<NotificationEntity>>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        return services;
    }
}