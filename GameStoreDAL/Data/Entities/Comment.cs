namespace Data.Entities
{
    public class Comment : BaseEntity
    {
        public int? ParentId { get; set; }
        public int? GameId { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public bool isDeleted { get; set; }
        public virtual ICollection<Comment>? Nested { get; set; }
        public virtual Comment? Parent { get; set; }
        public virtual Game? Game { get; set; }
    }
}
