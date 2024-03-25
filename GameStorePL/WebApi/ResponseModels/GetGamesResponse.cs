using WebApi.Models;

namespace WebApi.ResponseModels;

public class GetGamesResponse
{
    public ICollection<GameDTO> Games { get; set; }

    public string TotalPages { get; set; }

    public int CurrentPage { get; set; }
}
