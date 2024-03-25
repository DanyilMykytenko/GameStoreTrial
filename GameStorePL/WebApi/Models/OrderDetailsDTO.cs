namespace WebApi.Models;

public class OrderDetailsDTO
{
    public string ProductId { get; set; }

    public short Quantity { get; set; }

    public decimal Price { get; set; }

    public double Discount { get; set; }
}
