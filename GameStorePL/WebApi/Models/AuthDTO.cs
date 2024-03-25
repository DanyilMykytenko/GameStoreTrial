namespace WebApi.Models;

public class AuthDTO
{
    public string Login { get; set; }

    public string Password { get; set; }

    public bool InternalAuth { get; set; }
}
