using Business.Models;

namespace Business.Infrastructure.Interfaces
{
    public interface IGenreCategoryService
    {
        Task<IEnumerable<GenreModel>> GetByGameAsync(string key);
        Task DeleteAsync(int modelId);
        Task<IEnumerable<GenreModel>> GetAllAsync();
        Task<GenreModel> GetByIdAsync(int id);
        Task UpdateAsync(CreateUpdateGenreModel model);
    }
}
