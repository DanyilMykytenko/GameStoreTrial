namespace WebApi.Models;

public class GameDTO
{
    public string Id { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public short UnitInStock { get; set; }

    public int Discontinued { get; set; }
}