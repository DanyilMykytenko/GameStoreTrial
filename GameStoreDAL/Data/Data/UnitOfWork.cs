using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Data.Data;

public class UnitOfWork : IUnitOfWork
{
    private GameStoreDbContext Context { get; }
    private IUserRepository UserRepository;

    public IRepositoryFactory RepositoryFactory { get; }

    public UnitOfWork(
        IRepositoryFactory repositoryFactory,
        GameStoreDbContext context,
        IUserRepository userRepository)
    {
        RepositoryFactory = repositoryFactory;
        Context = context;
        UserRepository = userRepository;
    }

    public IUserRepository Users
    {
        get
        {
            if (UserRepository == null)
            {
                UserRepository = new UserRepository(Context);
            }

            return UserRepository;
        }
    }
    public async Task SaveAsync()
    {
        try
        {
            await Context.SaveChangesAsync();
        }
        catch(Exception exc)
        {
            throw exc.InnerException;
        }
    }

    public TRepository GetRepository<TEntity, TRepository>()
        where TEntity : BaseEntity
        where TRepository : class
    {
        return RepositoryFactory.Create<TEntity, TRepository>(Context);
    }
}
