using System.Linq.Expressions;

namespace Infra.Repositories;

public interface IRepository<TEntity> where TEntity : class, new()
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task CreateAsync(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(IEnumerable<TEntity> entities);
    Task<TEntity?> GetEntityAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FindAsync(int id);
    Task<IEnumerable<TEntity>> GetAsync();
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task UpdateAsync(TEntity entity);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);
}