namespace Data.Entities
{
    public class Publisher : BaseEntity
    {
        public string? CompanyName { get; set; }
        public string? Description { get; set; }
        public string? HomePage { get; set; }
        public string? FromMongo { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<Game>? Games { get; set; } = new HashSet<Game>();
    }
}
