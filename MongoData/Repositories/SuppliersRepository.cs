using MongoData.Entities;
using MongoData.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MongoData.Repositories
{
    public class SuppliersRepository : ISuppliersRepository
    {
        private readonly IMongoCollection<Supplier> _suppliers;
        private readonly IMongoCollection<Product> _products;
        public SuppliersRepository(IMongoDatabase database)
        {
            _suppliers = database.GetCollection<Supplier>(MongoCollectionNames.Suppliers);
            _products = database.GetCollection<Product>(MongoCollectionNames.Products);
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _suppliers.Find(Builders<Supplier>.Filter.Empty).ToListAsync();
        }

        public async Task<ICollection<Supplier>> GetAllAsync(Expression<Func<Supplier, bool>> expression)
        {
            return await _suppliers.Find<Supplier>(expression).ToListAsync();
        }

        public async Task<Supplier> GetByCompanyNameAsync(string companyName)
        {
            var filter = Builders<Supplier>.Filter.Where(x => x.CompanyName == companyName);
            var supplier = await _suppliers.Find(filter).FirstOrDefaultAsync();
            return supplier;
        }

        public async Task<Supplier> GetByGameAsync(string key)
        {
            var gameFilter = Builders<Product>.Filter.Where(x => x.Key == key);
            var game = await _products.Find(gameFilter).FirstOrDefaultAsync();

            var filter = Builders<Supplier>.Filter.Where(x => x.SupplierID == game.SupplierID);
            var supplier = await _suppliers.Find(filter).FirstOrDefaultAsync();

            return supplier;
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            var filter = Builders<Supplier>.Filter.Where(x => x.SupplierID == id);
            var supplier = await _suppliers.Find(filter).FirstOrDefaultAsync();
            return supplier;
        }
    }
}
