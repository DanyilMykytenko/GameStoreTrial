namespace WebApi.Models;

public class FilterDTO
{
    public ICollection<GameDTO> Games { get; set; }

    public string TotalPages { get; set; }

    public int CurrentPage { get; set; }
}
