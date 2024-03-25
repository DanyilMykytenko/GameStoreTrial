namespace WebApi.RequestModels;

public class UpdateRoleRequest
{
    public string Id { get; set; }

    public string Name { get; set; }

    public ICollection<string> Claims { get; set; }
}
