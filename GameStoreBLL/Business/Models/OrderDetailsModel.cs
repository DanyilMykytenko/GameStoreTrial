namespace Business.Models
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public string? ProductId { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }
        public double Discount { get; set; }
    }
}
