using MongoData.Entities;
using MongoData.Models;

namespace MongoData.Interfaces
{
    public interface IOrderRepository : ICrudMongo<Order>
    {
        Task<IEnumerable<Order>> GetAllInHistory(History model);
    }
}
