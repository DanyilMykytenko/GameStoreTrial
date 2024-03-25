namespace WebApi.Models;

public class GenreDTO
{
    public string Name { get; set; }

    public string? Id { get; set; }

    public string? ParentGenreId { get; set; }
}
