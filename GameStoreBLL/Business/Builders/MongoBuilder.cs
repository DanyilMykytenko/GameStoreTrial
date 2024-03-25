using MongoData.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Business.Builders
{
    public class MongoBuilder
    {
        private FilterDefinition<Product> _filter;

        public MongoBuilder()
        {
            _filter = Builders<Product>.Filter.Where(game => game.Key != string.Empty);
        }

        public void FilterByName(string name)
        {
            _filter &= Builders<Product>.Filter.Where(game => game.ProductName.Contains(name));
        }

        public void FilterByMaxPrice(int maxPrice, int minPrice)
        {
            _filter &= Builders<Product>.Filter.Where(game => game.UnitPrice >= minPrice && game.UnitPrice <= maxPrice);
        }

        public void FilterByMinPrice(int minPrice)
        {
            _filter &= Builders<Product>.Filter.Where(game => game.UnitPrice >= minPrice);
        }

        public void FilterByGenres(ICollection<string> Genres)
        {
            List<int> newCollection = Genres.Select(int.Parse).ToList();
            _filter &= Builders<Product>.Filter.Where(x => newCollection.Contains(x.CategoryID));
        }

        public void FilterByPublisher(ICollection<string> Publishers)
        {
            List<int> newCollection = Publishers.Select(int.Parse).ToList();
            _filter &= Builders<Product>.Filter.Where(x => newCollection.Contains(x.SupplierID));
        }

        public void FilterByPlatform()
        {
            _filter &= Builders<Product>.Filter.Size(game => game.Key, 0);
        }

        public FilterDefinition<Product> GetFilter()
        {
            return _filter;
        }
    }
}
