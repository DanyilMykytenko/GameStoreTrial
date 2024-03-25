using Microsoft.Extensions.Configuration;
using MongoData.Entities;
using MongoDB.Driver;

namespace MongoData.Data
{
    public class MongoDbContext
    {
        public IMongoCollection<Product> Products { get; set; }
        public IMongoCollection<Category> Categories { get; set; }
        public IMongoCollection<Shippers> Shippers { get; set; }
        public IMongoCollection<Supplier> Suppliers { get; set; }

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDb"]);
            var database = client.GetDatabase(configuration["MongoDbName"]);

            Products = database.GetCollection<Product>("Products");
            Categories = database.GetCollection<Category>("Categories");
            Shippers = database.GetCollection<Shippers>("Shippers");
            Suppliers = database.GetCollection<Supplier>("Suppliers");
        }
    }
}
