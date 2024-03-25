using Data.Entities;

namespace Business.Models;

public class GameModel
{
    public int Id { get; set; }

    public string? Key { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public short UnitInStock { get; set; }

    public int Discontinued { get; set; }
    public int? Viewed { get; set; }
    public DateTime CreationDate { get; set; }

    public ICollection<GenreModel>? Genres { get; set; }

    public ICollection<PlatformModel>? Platforms { get; set; }
    public PublisherModel? Publisher { get; set; }
    public ICollection<Comment>? Comments { get; set; } = new HashSet<Comment>();
}
