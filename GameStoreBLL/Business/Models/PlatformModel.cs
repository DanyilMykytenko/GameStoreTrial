using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class PlatformModel
{
    public int Id { get; set; }

    [Required]
    public string Type { get; set; }

    public ICollection<GameModel>? Games { get; set; }
}
