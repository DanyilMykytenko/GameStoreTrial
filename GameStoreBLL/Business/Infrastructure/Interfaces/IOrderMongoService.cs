using Business.Models;

namespace Business.Infrastructure.Interfaces
{
    public interface IOrderMongoService
    {
        Task<IEnumerable<OrderModel>> GetAllPaidOrders();
        Task<IEnumerable<OrderModel>> GetHistory(HistoryModel model);
        Task Download(int orderId);
        Task<OrderModel> GetOrderWithDetails(int orderId);
        Task<OrderModel> ExecuteTransaction(string option, VisaTransactionModel model);
    }
}
