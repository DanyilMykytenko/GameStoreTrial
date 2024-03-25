using MongoData.Entities;
using System.Linq.Expressions;

namespace MongoData.Interfaces
{
    public interface ICrudMongo<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetByIdAsync(int id);
    }
}
