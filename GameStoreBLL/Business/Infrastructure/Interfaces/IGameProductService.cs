using Business.Models;
using Data.Models;

namespace Business.Infrastructure.Interfaces
{
    public interface IGameProductService
    {
        Task AddAsync(CreateUpdateGameModel model); 
        Task DeleteByIdAsync(int id);
        Task<GetGamesResponseModel> GetAllAsync(GetGamesRequestModel model);
        Task<GameModel> GetByIdAsync(int id);
        Task<GameModel> GetByIdWithDetails(int id);
        Task<GameModel> GetByKeyAsync(string key);
        Task<GameModel> GetGameById(int id);
        Task<IEnumerable<GameModel>> SortAsync(Filter filter);
        Task Update(CreateUpdateGameModel entity);
        Task AddNewComment(string key, CreateUpdateCommentModel model);
        Task AddGameToBusket(string key);
        Task RemoveGameFromBusket(string key);
        Task<IEnumerable<GameModel>> GetByGenreAsync(int genreId);
        Task<IEnumerable<GameModel>> GetByPlatformAsync(int platformId);
        Task<IEnumerable<GameModel>> GetByPublisherAsync(string companyName);
        Task Download(string key);
    }
}
