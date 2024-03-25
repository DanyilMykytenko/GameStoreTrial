namespace MongoData.Entities
{
    public class Shippers : BaseEntity
    {
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
