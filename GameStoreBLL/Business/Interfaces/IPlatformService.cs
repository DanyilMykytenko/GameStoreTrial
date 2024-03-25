using Business.Models;

namespace Business.Interfaces;

public interface IPlatformService : ICrudService<PlatformModel, CreateUpdatePlatformModel>
{
    Task<IEnumerable<PlatformModel>> GetByGameAsync(string key);
}
