using AutoMapper;
using Business.Enums;
using Business.Infrastructure.Interfaces;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entities;
using Data.Interfaces;
using MongoData.Interfaces;

namespace Business.Infrastructure.Services
{
    public class GenreCategoryService : IGenreCategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreCategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IGenreService genreService, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _genreService = genreService;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int modelId)
        {
            var database = InfrastructureHelper.DefineDatabase(modelId, ConstService.GenreId);
            switch(database)
            {
                case DatabaseEnum.SQL:
                    await _genreService.DeleteAsync(modelId);
                    break;
                case DatabaseEnum.Mongo:
                    var genre = _mapper.Map<Genre>(await _categoryRepository.GetByIdAsync(modelId));
                    await _unitOfWork.GetRepository<Genre, IGenreRepository>().AddAsync(
                        new Genre
                        {
                            Name = genre.Name,
                            FromMongo = genre.Name,
                            IsDeleted = true
                        });
                    break;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<GenreModel>> GetAllAsync()
        {
            var genres = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync(x => x.IsDeleted == null);
            var categories = await _categoryRepository.GetAllAsync();
            var deletedGenres = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync(x => x.IsDeleted == true);

            var mappedCategories = _mapper.Map<IEnumerable<Genre>>(categories);

            var concated = mappedCategories.Where(x => !genres.Any(y => y.FromMongo == x.Name) && !deletedGenres.Any(d => d.FromMongo == x.Name));
            var result = concated.Concat(genres) as IEnumerable<Genre>;

            return _mapper.Map<IEnumerable<GenreModel>>(result);
        }

        public async Task<IEnumerable<GenreModel>> GetByGameAsync(string key)
        {
            var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
            var genre = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetByGameAsync(key);
            if (game is null)
            {
                var category = _mapper.Map<IEnumerable<Genre>>(await _categoryRepository.GetByGameAsync(key));
                var result = _mapper.Map<IEnumerable<GenreModel>>(category);
                return result;
            }
            return _mapper.Map<IEnumerable<GenreModel>>(genre);
        }

        public async Task<GenreModel> GetByIdAsync(int id)
        {
            var genre = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetByIdAsync(id);
            if(genre is null)
            {
                genre = _mapper.Map<Genre>(await _categoryRepository.GetByIdAsync(id));
            }

            return _mapper.Map<GenreModel>(genre);
        }

        public async Task UpdateAsync(CreateUpdateGenreModel model)
        {
            switch(InfrastructureHelper.DefineDatabase(model.Id.Value, ConstService.GenreId))
            {
                case DatabaseEnum.SQL:
                    await _genreService.UpdateAsync(model);
                    break;
                case DatabaseEnum.Mongo:
                    var category = await _categoryRepository.GetByIdAsync(model.Id.Value);
                    await _unitOfWork.GetRepository<Genre, IGenreRepository>().AddAsync(new Genre
                    {
                        Name = model.Name,
                        FromMongo = category.CategoryName
                    });
                    break;
            }
            await _unitOfWork.SaveAsync();
        }
        private string DefineDatabase(int id)
        {
            if (id >= ConstService.GenreId)
            {
                return DatabaseEnum.SQL.ToString();
            }
            else
            {
                return DatabaseEnum.Mongo.ToString();
            }
        }
    }
}
