namespace Business.Models
{
    public class PublisherModel
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Description { get; set;}
        public string? HomePage { get; set; }
        public ICollection<GameModel>? Games { get; set; }
    }
}
