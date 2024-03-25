using Business.Enums;
using Business.PaymentStrategies.Payments;

namespace Business.PaymentStrategies
{
    public class PaymentFactory : IPaymentFactory
    {
        public async Task<IStrategy> Create<TModel>(string option)
        {
            switch (option)
            {
                case nameof(PaymentOptionsEnum.Visa):
                    return new VisaPayment();
                case nameof(PaymentOptionsEnum.IBox):
                    return new IBoxPayment();
                case nameof(PaymentOptionsEnum.Bank):
                    return new BankPayment();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
