using Business.Models;
using Flurl;
using Flurl.Http;

namespace Business.PaymentStrategies.Payments
{
    public class VisaPayment : IStrategy
    {

        public async Task MakeTransaction(PaymentModel option)
        {
            var payment = await "http://localhost:5000/api/payments"
                .AppendPathSegment("visa")
                .PostJsonAsync(option);
        }
    }
}
