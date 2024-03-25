using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class BasketService : IBasketService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private const int customerId = 1;

        public BasketService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(BasketModel model)
        {
            if (model is null)
            {
                throw new GameStoreException("Model is null.");
            }
            var basket = _mapper.Map<Basket>(model);

            await _unitOfWork.GetRepository<Basket, IBasketRepository>().AddAsync(basket);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            if(modelId == 0) 
            {
                throw new GameStoreException("Id not found");
            }
            await _unitOfWork.GetRepository<Basket, IBasketRepository>().DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BasketModel>> GetAllAsync()
        {
            var basket = await _unitOfWork.GetRepository<Basket, IBasketRepository>().GetAllWithDetails();
            return _mapper.Map<IEnumerable<BasketModel>>(basket);
        }

        public async Task<BasketModel> GetByIdAsync(int id)
        {
            var basket = await _unitOfWork.GetRepository<Basket, IBasketRepository>().GetByIdAsync(id);
            return _mapper.Map<BasketModel>(basket);
        }

        public async Task<BasketModel> GetByIdWithDetails(int id)
        {
            var basket = await _unitOfWork.GetRepository<Basket, IBasketRepository>().GetByIdWithDetails(id);
            return _mapper.Map<BasketModel>(basket);
        }

        public async Task UpdateAsync(BasketModel model)
        {
            var basket = await _unitOfWork.GetRepository<Basket, IBasketRepository>().GetByIdAsync(model.Id);
            _mapper.Map(model, basket);

            _unitOfWork.GetRepository<Basket, IBasketRepository>().Update(basket);
            await _unitOfWork.SaveAsync();
        }
        public async Task AddGameToBasket(string key)
        {
            var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
            var order = new OrderDetails
            {
                Price = game.Price,
                Discount = game.Discontinued,
                Quantity = 1,
                ProductId = game.Id.ToString()
            };
            var basket = await _unitOfWork.GetRepository<Basket, IBasketRepository>().GetByIdWithDetails(1);
            if (basket is null)
            {
                await _unitOfWork.GetRepository<Basket, IBasketRepository>().AddAsync(
                    new Basket
                    {
                        Details = new List<OrderDetails>()
                        {
                            order
                        },
                        CustomerId = customerId
                    });
                await _unitOfWork.SaveAsync();
                return;
            }

            var detail = basket.Details.FirstOrDefault(x => x.ProductId == game.Id.ToString());
            if (detail is null)
            {
                basket.Details.Add(order);
            }
            else
            {
                basket.Details.FirstOrDefault(x => x.ProductId == game.Id.ToString()).Quantity += 1;
            }
            _unitOfWork.GetRepository<Basket, IBasketRepository>().Update(basket);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveGameFromBasket(string key)
        {
            var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
            var basket = await _unitOfWork.GetRepository<Basket, IBasketRepository>().GetByIdWithDetails(customerId);


            var details = basket.Details.First(x => x.ProductId == game.Id.ToString());
            basket.Details.Remove(details);
            await _unitOfWork.GetRepository<OrderDetails, IOrderDetailsRepository>().DeleteByIdAsync(details.Id);
            await _unitOfWork.SaveAsync();
        }
    }
}
