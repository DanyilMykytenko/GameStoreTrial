using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class PlatformService : IPlatformService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PlatformService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddAsync(CreateUpdatePlatformModel model)
    {
        var platform = _mapper.Map<Platform>(model);
        await _unitOfWork.GetRepository<Platform, IPlatformRepository>().AddAsync(platform);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int modelId)
    {
        if(modelId == 0) 
        {
            throw new GameStoreException("Id is null");
        }
        await _unitOfWork.GetRepository<Platform, IPlatformRepository>().DeleteByIdAsync(modelId);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<PlatformModel>> GetAllAsync()
    {
        var platforms = await _unitOfWork.GetRepository<Platform, IPlatformRepository>().GetAllAsync();
        return _mapper.Map<IEnumerable<PlatformModel>>(platforms);
    }

    public async Task<IEnumerable<PlatformModel>> GetByGameAsync(string key)
    {
        var platforms = await _unitOfWork.GetRepository<Platform, IPlatformRepository>().GetByGameAsync(key);
        return _mapper.Map<IEnumerable<PlatformModel>>(platforms);
    }

    public async Task<PlatformModel> GetByIdAsync(int id)
    {
        if (id == 0)
        {
            throw new GameStoreException("Id is null");
        }
        var platform = await _unitOfWork.GetRepository<Platform, IPlatformRepository>().GetByIdAsync(id);
        if (platform is null)
        {
            throw new NotFoundException(nameof(platform));
        }
        return _mapper.Map<PlatformModel>(platform);
    }

    public async Task UpdateAsync(CreateUpdatePlatformModel model)
    {
        if(model is null)
        {
            throw new GameStoreException("Model is null");
        }
        var platform = await _unitOfWork.GetRepository<Platform, IPlatformRepository>().GetByIdAsync(model.Id.Value);
        if (platform is null)
        {
            throw new NotFoundException(nameof(platform));
        }
        _mapper.Map(model, platform);
        _unitOfWork.GetRepository<Platform, IPlatformRepository>().Update(platform);
        await _unitOfWork.SaveAsync();
    }
}
