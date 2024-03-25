namespace Data.Entities;

public class Platform : BaseEntity
{
    public string? Type { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
}
