namespace Business.Models
{
    public class UpdateRoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> Claims { get; set; }
    }
}
