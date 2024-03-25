using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.PaymentStrategies;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;
using System.Globalization;
using System.Text;

namespace Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentFactory _paymentFactory;
        public const int customerId = 1;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IPaymentFactory paymentFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentFactory = paymentFactory;
        }
        public async Task AddAsync(CreateUpdateOrderModel model)
        {
            if (model is null)
            {
                throw new GameStoreException("Model is null.");
            }
            var order = _mapper.Map<Order>(model);
            await _unitOfWork.GetRepository<Order, IOrderRepository>().AddAsync(order);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            if (modelId == 0)
            {
                throw new GameStoreException("Id is null.");
            }
            await _unitOfWork.GetRepository<Order, IOrderRepository>().DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task Download(int orderId)
        {
            if (orderId == 0)
            {
                throw new GameStoreException("OrderId is null!");
            }
            var order = await _unitOfWork.GetRepository<Order, IOrderRepository>().GetOrderByIdWithDetails(orderId);
            if (order is null)
            {
                throw new NotFoundException(nameof(order));
            }
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Order-{order.Id}.txt");
            using FileStream fs = System.IO.File.Create(path);
            var text = Encoding.ASCII.GetBytes($"{order.Id}: {order.OrderDate}");
            await fs.WriteAsync(text);
        }
        public async Task<IEnumerable<OrderModel>> GetHistory(HistoryModel model)
        {
            if (string.IsNullOrEmpty(model.Start))
            {
                return new List<OrderModel>();
            }

            var orders = await _unitOfWork.GetRepository<Order, IOrderRepository>().GetAllAsync();
            return _mapper.Map<IEnumerable<OrderModel>>(orders);
        }
        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            var orders = await _unitOfWork.GetRepository<Order, IOrderRepository>().GetAllAsync();
            return _mapper.Map<IEnumerable<OrderModel>>(orders);
        }

        public async Task<IEnumerable<OrderModel>> GetAllPaidOrders()
        {
            var orders = await _unitOfWork.GetRepository<Order, IOrderRepository>().GetAllPaidOrders(customerId);
            return _mapper.Map<IEnumerable<OrderModel>>(orders);
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                throw new GameStoreException("Id is null");
            }
            var order = await _unitOfWork.GetRepository<Order, IOrderRepository>().GetByIdAsync(id);
            if (order is null)
            {
                throw new NotFoundException(nameof(order));
            }
            return _mapper.Map<OrderModel>(order);
        }

        public async Task<OrderModel> GetOrderWithDetails(int orderId)
        {
            var order = await _unitOfWork.GetRepository<Order, IOrderRepository>().GetOrderByIdWithDetails(orderId);
            return _mapper.Map<OrderModel>(order);
        }

        public async Task UpdateAsync(CreateUpdateOrderModel model)
        {
            if (model is null)
            {
                throw new GameStoreException("Model is null.");
            }
            var order = await _unitOfWork.GetRepository<Order, IOrderRepository>().GetByIdAsync(model.Id.Value);
            if (order is null)
            {
                throw new NotFoundException(nameof(order));
            }
            _mapper.Map(model, order);
            _unitOfWork.GetRepository<Order, IOrderRepository>().Update(order);
            await _unitOfWork.SaveAsync();
        }

        public async Task<OrderModel> ExecuteTransaction(string option, VisaTransactionModel model)
        {
            if (option == "IBox terminal")
            {
                option = "IBox";
            }

            var busket = await _unitOfWork.GetRepository<Basket, IBasketRepository>().GetByIdWithDetails(1);

            var payment = await _paymentFactory.Create<PaymentModel>(option);
            await payment.MakeTransaction(model);

            await _unitOfWork.GetRepository<Order, IOrderRepository>().AddAsync(
                new Order
                {
                    Details = busket.Details,
                    CustomerId = customerId
                });
            await _unitOfWork.SaveAsync();
            var order = await _unitOfWork.GetRepository<Order, IOrderRepository>().GetLastOrderWithDetails();
            return _mapper.Map<OrderModel>(order);
        }
        private DateTime GetCorrectDateTime(string dateString)
        {
            var gmtLength = dateString.Length - dateString.IndexOf("GMT") + 1;
            var correctDateTime = dateString.Remove(dateString.Length - gmtLength);

            var datetime = DateTime.ParseExact(correctDateTime,
                "ddd MMM dd yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return datetime;
        }

    }
}
