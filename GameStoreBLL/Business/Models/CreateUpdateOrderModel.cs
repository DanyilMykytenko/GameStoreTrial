namespace Business.Models
{
    public class CreateUpdateOrderModel
    {
        public int? Id { get; set; }
        public string? ProductId { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }
        public double Discount { get; set; }
        public bool isPaid { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
    }
}
