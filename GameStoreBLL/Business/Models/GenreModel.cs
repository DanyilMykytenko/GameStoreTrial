namespace Business.Models;

public class GenreModel
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public int? ParentId { get; set; }
    public ICollection<GameModel>? Games { get; set; }
    public ICollection<GenreModel>? Nested { get; set; }
    public GenreModel? Parent { get; set; }
}
