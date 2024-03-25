using Business.Models;
using Flurl;
using Flurl.Http;

namespace Business.PaymentStrategies.Payments
{
    public class IBoxPayment : IStrategy
    {
        public async Task MakeTransaction(PaymentModel option)
        {
            var payment = await "http://localhost:5000/api/payments"
                .AppendPathSegment("ibox")
                .PostJsonAsync(option);
        }
    }
}
