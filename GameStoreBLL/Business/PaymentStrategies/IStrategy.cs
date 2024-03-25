using Business.Models;

namespace Business.PaymentStrategies
{
    public interface IStrategy
    {
        Task MakeTransaction(PaymentModel model);
    }
}
