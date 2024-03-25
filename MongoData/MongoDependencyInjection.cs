using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoData.Interfaces;
using MongoData.Repositories;
using MongoDB.Driver;

namespace MongoData
{
    public static class MongoDependencyInjection
    {
        public static void AddMongoDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddSingleton(new MongoClient(configuration["MongoDb"])
                .GetDatabase(configuration["MongoDbName"]));

            services.AddScoped(typeof(ICrudMongo<>), typeof(CrudMongo<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISuppliersRepository, SuppliersRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<Interfaces.IOrderRepository, Repositories.OrderRepository>();
        }
    }
}
