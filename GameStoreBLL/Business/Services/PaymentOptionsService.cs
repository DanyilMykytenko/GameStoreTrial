using Business.Enums;
using Business.Models;

namespace Business.Services
{
    public static class PaymentOptionsService
    {
        public static List<PaymentOptionsModel> Options { get; } = new List<PaymentOptionsModel>
        {
            PaymentOptionsEnum.Visa.Model,
            PaymentOptionsEnum.IBox.Model,
            PaymentOptionsEnum.Bank.Model
        };
    }
}
