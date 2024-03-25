using MongoDB.Bson.Serialization.Attributes;

namespace MongoData.Entities
{
    [BsonIgnoreExtraElements]
    public class Order : BaseEntity
    {
        public int OrderID { get; set; }
        public string OrderDate { get; set; }

    }
}
