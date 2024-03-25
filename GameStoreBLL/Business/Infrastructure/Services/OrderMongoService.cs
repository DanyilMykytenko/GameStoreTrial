using AutoMapper;
using Business.Infrastructure.Interfaces;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Globalization;

namespace Business.Infrastructure.Services
{
    public class OrderMongoService : IOrderMongoService
    {
        private readonly MongoData.Interfaces.IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderMongoService(MongoData.Interfaces.IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderService orderService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task Download(int orderId)
        {
            await _orderService.Download(orderId);
        }

        public async Task<OrderModel> ExecuteTransaction(string option, VisaTransactionModel model)
        {
            return await _orderService.ExecuteTransaction(option, model);
        }

        public async Task<IEnumerable<OrderModel>> GetAllPaidOrders()
        {
            return await _orderService.GetAllPaidOrders();
        }

        public async Task<IEnumerable<OrderModel>> GetHistory(HistoryModel model)
        {
            var mongoOrders = await _orderRepository.GetAllAsync();
            var sqlOrders = await _unitOfWork.GetRepository<Order, Data.Interfaces.IOrderRepository>().GetAllAsync();

            var mappedOrders = _mapper.Map<IEnumerable<Order>>(mongoOrders);
            var concated = sqlOrders.Concat(mappedOrders);

            var result = concated.Where(x => x.OrderDate >= GetCorrectDateTime(model.Start) && x.OrderDate <= GetCorrectDateTime(model.End));

            return _mapper.Map<IEnumerable<OrderModel>>(result);
        }

        public async Task<OrderModel> GetOrderWithDetails(int orderId)
        {
            return await _orderService.GetOrderWithDetails(orderId);
        }
        private DateTime GetCorrectDateTime(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return DateTime.UtcNow;
            }

            var gmtLength = dateString.Length - dateString.IndexOf("GMT") + 1;
            var correctDateTime = dateString.Remove(dateString.Length - gmtLength);

            var datetime = DateTime.ParseExact(correctDateTime,
                "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return datetime;
        }
    }
}
