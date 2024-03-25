using Data.Entities;

namespace Data.Interfaces;

public interface IUnitOfWork
{
    IRepositoryFactory RepositoryFactory { get; }

    public TRepository GetRepository<TEntity, TRepository>() where TEntity : BaseEntity where TRepository : class;
    Task SaveAsync();
}
