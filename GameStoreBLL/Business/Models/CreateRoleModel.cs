namespace Business.Models
{
    public class CreateRoleModel
    {
        public string Name { get; set; }
        public ICollection<string> Claims { get; set; }
    }
}
