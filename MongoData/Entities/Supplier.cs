using MongoDB.Bson.Serialization.Attributes;

namespace MongoData.Entities
{
    [BsonIgnoreExtraElements]
    public class Supplier : BaseEntity
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public string HomePage { get; set; }
        public string Address { get; set; }
    }
}
