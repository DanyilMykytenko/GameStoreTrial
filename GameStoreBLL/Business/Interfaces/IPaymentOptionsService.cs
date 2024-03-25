using Business.Models;

namespace Business.Interfaces
{
    public interface IPaymentOptionsService
    {
        IEnumerable<PaymentOptionsModel> GetAll();
    }
}
