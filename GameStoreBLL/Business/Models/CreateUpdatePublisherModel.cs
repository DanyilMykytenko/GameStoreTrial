namespace Business.Models
{
    public class CreateUpdatePublisherModel
    {
        public int? Id { get; set; }
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string? HomePage { get; set; }
    }
}
