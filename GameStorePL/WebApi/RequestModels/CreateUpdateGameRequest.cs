using WebApi.Models;

namespace WebApi.RequestModels;

public class CreateUpdateGameRequest
{
    public GameDTO Game { get; set; }

    public List<string>? Genres { get; set; }

    public List<string>? Platforms { get; set; }

    public string? Publisher { get; set; }
}
