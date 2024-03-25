using WebApi.Models;

namespace WebApi.RequestModels;

public class UpdateUserRequest
{
    public ICollection<string> Roles { get; set; }

    public UserDTO User { get; set; }
}
