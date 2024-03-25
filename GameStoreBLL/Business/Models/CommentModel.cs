namespace Business.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Body { get; set; }

        public int? ParentId { get; set; }
        public GameModel? Game { get; set; }
        public ICollection<CommentModel>? Nested { get; set; }
        public CommentModel? Parent { get; set; }
    }
}
