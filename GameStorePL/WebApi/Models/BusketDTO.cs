namespace WebApi.Models;

public class BusketDTO
{
    public int Id { get; set; }

    public ICollection<OrderDetailsDTO>? Details { get; set; }

    public virtual int? CustomerId { get; set; }
}
