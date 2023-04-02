using AutoMapper;
using Scrooge.DbServices;

namespace Scrooge.Services.Services;

public class Service<TModel, TEntity> : IService<TModel, TEntity> where TModel : class where TEntity : class
{
    public IDbUnitOfWork<TEntity> DbUnitOfWork { get; set; }
    public IMapper Mapper { get; set; }

    public Service(IDbUnitOfWork<TEntity> unitOfWork, IMapper mapper)
    {
        DbUnitOfWork = unitOfWork;
        Mapper = mapper;
    }
    
    public async Task<IList<TModel>> Get()
    {
        var entity = await DbUnitOfWork.Repository.GetAsync();
        return Mapper.Map<IList<TModel>>(entity);
    }
}