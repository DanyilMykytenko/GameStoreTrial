using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PublisherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateUpdatePublisherModel model)
        {
            var publisher = _mapper.Map<Publisher>(model);
            await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().AddAsync(publisher);
            await _unitOfWork.SaveAsync();
        }
    
        public async Task DeleteAsync(int modelId)
        {
            if (modelId == 0)
            {
                throw new GameStoreException("Id is null");
            }
            var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByIdWithDetails(modelId);
            await RemoveRelatedData(publisher.Games, publisher);

            await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().DeleteByIdAsync(modelId);
            
            await _unitOfWork.SaveAsync();
        }
    
        public async Task<IEnumerable<PublisherModel>> GetAllAsync()
        {
            var publishers = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetAllAsync();
            return _mapper.Map<IEnumerable<PublisherModel>>(publishers);
        }

        public async Task<PublisherModel> GetByCompanyNameAsync(string companyName)
        {
            var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByCompanyNameAsync(companyName);
            return _mapper.Map<PublisherModel>(publisher);
        }

        public async Task<PublisherModel> GetByGameAsync(string key)
        {
            var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByGameAsync(key);
            return _mapper.Map<PublisherModel>(publisher);
        }

        public async Task<PublisherModel> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                throw new GameStoreException("Id is null");
            }
            var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByIdAsync(id);
            if (publisher is null)
            {
                throw new NotFoundException(nameof(publisher));
            }
            return _mapper.Map<PublisherModel>(publisher);
        }
    
        public async Task UpdateAsync(CreateUpdatePublisherModel model)
        {
            if (model is null)
            {
                throw new GameStoreException("Model is null");
            }
            var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByIdAsync(model.Id.Value);
            if (publisher is null)
            {
                throw new NotFoundException(nameof(publisher));
            }
            _mapper.Map(model, publisher);
            _unitOfWork.GetRepository<Publisher, IPublisherRepository>().Update(publisher);
            await _unitOfWork.SaveAsync();
        }
        private async Task RemoveRelatedData(ICollection<Game> gamesList, Publisher publisher)
        {
            var games = await _unitOfWork.GetRepository<Game, IGameRepository>().GetAllAsync(x => gamesList.Contains(x));

            foreach (var game in games)
            {
                game.Publisher = null;
                _unitOfWork.GetRepository<Game, IGameRepository>().Update(game);
            }
        }
    }
}
