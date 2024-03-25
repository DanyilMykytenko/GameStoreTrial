using Business.Models;
using System.Text;

namespace Business.PaymentStrategies.Payments
{
    public class BankPayment : IStrategy
    {
        public async Task MakeTransaction(PaymentModel option)
        {
            var transactionAmount = option?.TransactionAmount ?? 0;
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Order.txt");
            using FileStream fs = System.IO.File.Create(path);
            var text = Encoding.ASCII.GetBytes($"{transactionAmount}");
            await fs.WriteAsync(text);
        }
    }
}
