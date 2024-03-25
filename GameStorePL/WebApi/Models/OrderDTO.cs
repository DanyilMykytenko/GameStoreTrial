namespace WebApi.Models;

public class OrderDTO
{
    public string UserId { get; set; }

    public string OrderId { get; set; }

    public string Sum { get; set; }

    public DateTime OrderDate { get; set; }
}
