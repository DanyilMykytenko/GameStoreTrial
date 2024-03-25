using WebApi.Models;

namespace WebApi.RequestModels;

public class CreateUserRequest
{
    public string Password { get; set; }

    public ICollection<string> Roles { get; set; }

    public UserDTO User { get; set; }
}
