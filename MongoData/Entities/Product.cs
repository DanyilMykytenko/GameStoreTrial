using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoData.Entities
{
    public class Product : BaseEntity
    {
        public string Key { get; set; }
        public int? Viewed { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int Discontinued { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
    }
}
