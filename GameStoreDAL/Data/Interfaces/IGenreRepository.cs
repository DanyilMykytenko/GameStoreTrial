using Data.Entities;

namespace Data.Interfaces;

public interface IGenreRepository : IRepository<Genre>
{
    Task<IEnumerable<Genre>> GetByGameAsync(string key);
}
