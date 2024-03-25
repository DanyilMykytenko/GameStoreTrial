using WebApi.Models;

namespace WebApi.RequestModels;

public class CreateUpdateOrderRequest
{
    public OrderDTO Order { get; set; }
}
