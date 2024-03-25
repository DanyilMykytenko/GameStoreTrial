namespace WebApi.RequestModels;

public class CreateRoleRequest
{
    public string Name { get; set; }

    public ICollection<string> Claims { get; set; }
}
