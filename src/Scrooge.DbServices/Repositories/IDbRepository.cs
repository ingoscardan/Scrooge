using System.Linq.Expressions;

namespace Scrooge.DbServices.Repositories;

public interface IDbRepository<TEntity> where TEntity : class
{
    Task<IList<TEntity>> GetAsync();
    Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> whereCondition = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);

    Task<TEntity?> GetByIdAsync(object id);
}