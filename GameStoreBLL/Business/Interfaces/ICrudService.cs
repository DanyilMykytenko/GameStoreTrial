namespace Business.Interfaces;

public interface ICrudService<TModel, TCreateUpdate> where TModel : class where TCreateUpdate : class
{
    Task<IEnumerable<TModel>> GetAllAsync();

    Task<TModel> GetByIdAsync(int id);

    Task AddAsync(TCreateUpdate model);

    Task UpdateAsync(TCreateUpdate model);

    Task DeleteAsync(int modelId);
}
