using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoData.Entities
{
    [BsonIgnoreExtraElements]
    public class BaseEntity
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
    }
}
