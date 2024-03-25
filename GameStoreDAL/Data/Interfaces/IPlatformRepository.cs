using Data.Entities;

namespace Data.Interfaces;

public interface IPlatformRepository : IRepository<Platform>
{
    Task<IEnumerable<Platform>> GetByGameAsync(string key);
}
