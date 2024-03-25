namespace Business.Models
{
    public class UserModel
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<string> Roles { get; set; } 
    }
}
