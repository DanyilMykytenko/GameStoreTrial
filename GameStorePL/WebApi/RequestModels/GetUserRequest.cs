namespace WebApi.RequestModels;

public class GetUserRequest
{
    public string Id { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public ICollection<string> Roles { get; set; }
}
