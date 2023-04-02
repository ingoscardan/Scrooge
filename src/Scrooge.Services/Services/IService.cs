using AutoMapper;
using Scrooge.DbServices;

namespace Scrooge.Services.Services;

public interface IService<TModel, TEntity> where TModel : class where TEntity : class
{
    IDbUnitOfWork<TEntity> DbUnitOfWork { get; set; }
    public IMapper Mapper { get; set; }

    Task<IList<TModel>> Get();
}