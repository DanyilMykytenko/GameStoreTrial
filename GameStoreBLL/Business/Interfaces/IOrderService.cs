using Business.Models;

namespace Business.Interfaces
{
    public interface IOrderService : ICrudService<OrderModel, CreateUpdateOrderModel>
    {
        Task<IEnumerable<OrderModel>> GetAllPaidOrders();
        Task<IEnumerable<OrderModel>> GetHistory(HistoryModel model);
        Task Download(int orderId);
        Task<OrderModel> GetOrderWithDetails(int orderId);
        Task<OrderModel> ExecuteTransaction(string option, VisaTransactionModel model);
    }
}
