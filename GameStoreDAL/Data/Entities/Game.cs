namespace Data.Entities;

public class Game : BaseEntity
{
    public string? Key { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
    public decimal Price { get; set; }
    public short UnitInStock { get; set; }
    public int Discontinued { get; set; }

    public int? PublisherId { get; set; }
    public bool IsDeleted { get; set; }
    public int Viewed { get; set; }
    public string? FromMongo { get; set; }
    public DateTime CreationDate { get; set; }
    public virtual ICollection<Genre>? Genres { get; set; } = new HashSet<Genre>();

    public virtual ICollection<Platform>? Platforms { get; set; } = new HashSet<Platform>();
    public virtual ICollection<Comment>? Comments { get; set; } = new HashSet<Comment>();
    public virtual Publisher? Publisher { get; set; }
}
