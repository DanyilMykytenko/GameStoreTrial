using Business.Models;

namespace Business.Interfaces
{
    public interface IGameService : ICrudService<GameModel, CreateUpdateGameModel>
    {
        Task<GetGamesResponseModel> GetAllAsync(GetGamesRequestModel model);
        Task<GameModel> GetByKeyAsync(string key);

        Task<IEnumerable<GameModel>> GetByGenreAsync(int genreId);

        Task<IEnumerable<GameModel>> GetByPlatformAsync(int platformId);
        Task<IEnumerable<GameModel>> GetByPublisherAsync(string companyName);
        Task Download(string key);
    }
}
