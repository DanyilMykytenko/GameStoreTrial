using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddAsync(CreateUpdateGenreModel model)
    {
        if (model is null)
        {
            throw new GameStoreException("Model is null.");
        }
        var genre = _mapper.Map<Genre>(model);
        await _unitOfWork.GetRepository<Genre, IGenreRepository>().AddAsync(genre);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int modelId)
    {
        if (modelId == 0)
        {
            throw new GameStoreException("Id is null.");
        }
        await _unitOfWork.GetRepository<Genre, IGenreRepository>().DeleteByIdAsync(modelId);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<GenreModel>> GetAllAsync()
    {
        var genres = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync();
        return _mapper.Map<IEnumerable<GenreModel>>(genres);
    }

    public async Task<IEnumerable<GenreModel>> GetByGameAsync(string key)
    {
        var genres = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetByGameAsync(key);
        return _mapper.Map<IEnumerable<GenreModel>>(genres);
    }

    public async Task<GenreModel> GetByIdAsync(int id)
    {
        if (id == 0)
        {
            throw new GameStoreException("Id is null");
        }
        var genre = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetByIdAsync(id);
        if (genre is null)
        {
            throw new NotFoundException(nameof(genre));
        }
        return _mapper.Map<GenreModel>(genre);
    }

    public async Task UpdateAsync(CreateUpdateGenreModel model)
    {
        if (model is null)
        {
            throw new GameStoreException("Model is null.");
        }
        var genre = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetByIdAsync(model.Id.Value);
        if (genre is null)
        {
            throw new NotFoundException(nameof(genre));
        }
        _mapper.Map(model, genre);
        _unitOfWork.GetRepository<Genre, IGenreRepository>().Update(genre);
        await _unitOfWork.SaveAsync();
    }
}
