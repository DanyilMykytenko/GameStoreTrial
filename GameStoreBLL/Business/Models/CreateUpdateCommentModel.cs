namespace Business.Models
{
    public class CreateUpdateCommentModel
    {
        public string Action { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }

        public int? ParentId { get; set; }
    }
}
