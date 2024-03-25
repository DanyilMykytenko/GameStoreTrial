using Data.Data;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IRepositoryFactory
    {
        public TRepository Create<TEntity, TRepository>(GameStoreDbContext dbContext) where TEntity : BaseEntity where TRepository : class;
    }

}
