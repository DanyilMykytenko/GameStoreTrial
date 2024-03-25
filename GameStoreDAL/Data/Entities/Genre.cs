namespace Data.Entities;

public class Genre : BaseEntity
{
    public int? ParentId { get; set; }

    public string? Name { get; set; }
    public string? FromMongo { get; set; }
    public bool? IsDeleted { get; set; }
    public virtual ICollection<Game>? Games { get; set; } = new HashSet<Game>();

    public virtual ICollection<Genre>? Nested { get; set; }

    public virtual Genre? Parent { get; set; }
}
