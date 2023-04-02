using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Scrooge.DbServices.DBContext;

namespace Scrooge.DbServices.Repositories;

public class DbRepository<TEntity>: IDbRepository<TEntity> where TEntity: class
{
    protected readonly ScroogeDbContext Context;

    public DbRepository(ScroogeDbContext context)
    {
        Context = context;
    }

    public async Task<IList<TEntity>> GetAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> whereCondition = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        if (whereCondition != null)
        {
            query = query.Where(whereCondition);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
            
    }

    public async Task<TEntity?> GetByIdAsync(object id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }
}