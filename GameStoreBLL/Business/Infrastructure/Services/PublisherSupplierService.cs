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
    public class PublisherSupplierService : IPublisherSupplierService
    {
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherSupplierService(ISuppliersRepository suppliersRepository, IUnitOfWork unitOfWork, IPublisherService publisherService, IMapper mapper)
        {
            _suppliersRepository = suppliersRepository;
            _publisherService = publisherService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int modelId)
        {
            var database = InfrastructureHelper.DefineDatabase(modelId, ConstService.PublisherId);
            switch (database)
            {
                case DatabaseEnum.SQL:
                    await _publisherService.DeleteAsync(modelId);
                    break;
                case DatabaseEnum.Mongo:
                    var publisher = _mapper.Map<Publisher>(await _suppliersRepository.GetByIdAsync(modelId));
                    await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().AddAsync(new Publisher
                    {
                        CompanyName = publisher.CompanyName,
                        HomePage = publisher.HomePage,
                        Description = publisher.Description,
                        FromMongo = publisher.CompanyName,
                        IsDeleted = true
                    });
                    break;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<PublisherModel>> GetAllAsync()
        {
            var publishers = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetAllAsync(x => x.IsDeleted == null);
            var suppliers = await _suppliersRepository.GetAllAsync();
            var deletedPublishers = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetAllAsync(x => x.IsDeleted == true);

            var mappedSuppliers = _mapper.Map<IEnumerable<Publisher>>(suppliers);

            var concated = mappedSuppliers.Where(x => !publishers.Any(y => y.FromMongo == x.CompanyName) && !deletedPublishers.Any(d => d.FromMongo == x.CompanyName));
            var result = publishers.Concat(concated) as IEnumerable<Publisher>;

            return _mapper.Map<IEnumerable<PublisherModel>>(result);
        }

        public async Task<PublisherModel> GetByCompanyNameAsync(string companyName)
        {
            var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByCompanyNameAsync(companyName);
            if (publisher is null)
            {
                publisher = _mapper.Map<Publisher>(await _suppliersRepository.GetByCompanyNameAsync(companyName));
            }

            return _mapper.Map<PublisherModel>(publisher);
        }

        public async Task<PublisherModel> GetByGameAsync(string key)
        {
            var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
            var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByGameAsync(key);
            if (game is null)
            {
                var supplier = _mapper.Map<Publisher>(await _suppliersRepository.GetByGameAsync(key));
                var result = _mapper.Map<PublisherModel>(supplier);
                return result;
            }
            return _mapper.Map<PublisherModel>(publisher);
        }

        public async Task<PublisherModel> GetByIdAsync(int id)
        {
            var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByIdAsync(id);
            if (publisher is null)
            {
                publisher = _mapper.Map<Publisher>(await _suppliersRepository.GetByIdAsync(id));
            }

            return _mapper.Map<PublisherModel>(publisher);
        }

        public async Task UpdateAsync(CreateUpdatePublisherModel model)
        {
            switch (InfrastructureHelper.DefineDatabase(model.Id.Value, ConstService.PublisherId))
            {
                case DatabaseEnum.SQL:
                    await _publisherService.UpdateAsync(model);
                    break;
                case DatabaseEnum.Mongo:
                    var publisher = await _suppliersRepository.GetByIdAsync(model.Id.Value);
                    await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().AddAsync(new Publisher
                    {
                        CompanyName = publisher.CompanyName,
                        HomePage = publisher.HomePage,
                        Description = publisher.Address,
                        FromMongo = publisher.CompanyName
                    });
                    break;
            }
            await _unitOfWork.SaveAsync();
        }
        private string DefineDatabase(int id)
        {
            if (id >= ConstService.PublisherId)
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
