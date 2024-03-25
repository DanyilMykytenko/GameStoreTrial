using Business.Models;

namespace Business.Interfaces;

public interface IGenreService : ICrudService<GenreModel, CreateUpdateGenreModel>
{
    Task<IEnumerable<GenreModel>> GetByGameAsync(string key);
}
