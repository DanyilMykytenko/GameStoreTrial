using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public RepositoryFactory(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }
        public TRepository? Create<TEntity, TRepository>(GameStoreDbContext dbContext) where TEntity : BaseEntity where TRepository : class
        {
            switch (typeof(TEntity))
            {
                case Type type when type == typeof(Game):
                    return _serviceProvider.GetRequiredService<IGameRepository>() as TRepository;
                case Type type when type == typeof(Genre):
                    return _serviceProvider.GetRequiredService<IGenreRepository>() as TRepository;
                case Type type when type == typeof(Platform):
                    return _serviceProvider.GetRequiredService<IPlatformRepository>() as TRepository;
                case Type type when type == typeof(Publisher):
                    return _serviceProvider.GetRequiredService<IPublisherRepository>() as TRepository;
                case Type type when type == typeof(Order):
                    return _serviceProvider.GetRequiredService<IOrderRepository>() as TRepository;
                case Type type when type == typeof(Basket):
                    return _serviceProvider.GetRequiredService<IBasketRepository>() as TRepository;
                case Type type when type == typeof(OrderDetails):
                    return _serviceProvider.GetRequiredService<IOrderDetailsRepository>() as TRepository;
                case Type type when type == typeof(Comment):
                    return _serviceProvider.GetRequiredService<ICommentRepository>() as TRepository;
                default:
                    throw new ArgumentException($"Repository for entity type {typeof(TEntity)} not found");
            }
        }
    }

}
