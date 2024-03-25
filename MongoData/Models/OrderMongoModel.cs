using Data.Entities;

namespace MongoData.Models
{
    public class OrderMongoModel : BaseEntity
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
