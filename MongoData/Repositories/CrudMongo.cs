using MongoData.Entities;
using MongoData.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MongoData.Repositories
{
    public class CrudMongo<TEntity> : ICrudMongo<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IMongoCollection<TEntity> _entityCollection;
        public CrudMongo(IMongoCollection<TEntity> entityCollection)
        {
            _entityCollection = entityCollection;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entityCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _entityCollection.Find(expression).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Where(x => x.Id.ToString() == id.ToString());
            return await _entityCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
