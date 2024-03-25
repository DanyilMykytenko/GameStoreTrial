using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);

    Task<TEntity> GetByIdAsync(int id);

    Task AddAsync(TEntity entity);

    Task DeleteByIdAsync(int id);

    void Update(TEntity entity);
}
