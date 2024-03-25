using MongoData.Entities;
using MongoData.Interfaces;
using MongoData.Models;
using MongoDB.Driver;
using System.Globalization;
using System.Linq.Expressions;

namespace MongoData.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;
        public OrderRepository(IMongoDatabase database)
        {
            _orders = database.GetCollection<Order>(MongoCollectionNames.Orders);
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orders.Find(Builders<Order>.Filter.Empty).ToListAsync();
        }

        public async Task<ICollection<Order>> GetAllAsync(Expression<Func<Order, bool>> expression)
        {
            return await _orders.Find<Order>(expression).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllInHistory(History model)
        {
            if (string.IsNullOrEmpty(model.Start))
            {
                return new List<Order>();
            }

            var filter = Builders<Order>.Filter.And(
                Builders<Order>.Filter.Gte(nameof(Order.OrderDate), GetCorrectDateTime(model.Start).ToString()),
                Builders<Order>.Filter.Lte(nameof(Order.OrderDate), GetCorrectDateTime(model.End).ToString()));

            var orders = await _orders.Find<Order>(x => DateTime.Parse(x.OrderDate) >= DateTime.Parse(model.Start) 
                && DateTime.Parse(x.OrderDate) <= DateTime.Parse(model.End)).ToListAsync();
            return orders;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var filter = Builders<Order>.Filter.Where(x => x.OrderID == id);
            var order = await _orders.Find(filter).FirstOrDefaultAsync();
            return order;
        }
        private DateTime GetCorrectDateTime(string dateString)
        {
            var gmtLength = dateString.Length - dateString.IndexOf("GMT") + 1;
            var correctDateTime = dateString.Remove(dateString.Length - gmtLength);

            var datetime = DateTime.ParseExact(correctDateTime,
                "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return datetime;
        }
    }
}
