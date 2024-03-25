namespace Data.Entities
{
    public class OrderDetails : BaseEntity
    {
        public string? ProductId { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }
        public double Discount { get; set; }
    }
}
