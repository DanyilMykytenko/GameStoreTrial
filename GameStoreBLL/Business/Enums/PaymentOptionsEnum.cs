using Ardalis.SmartEnum;
using Business.Models;

namespace Business.Enums
{
    public sealed class PaymentOptionsEnum : SmartEnum<PaymentOptionsEnum>
    {
        public static readonly PaymentOptionsEnum Visa = new("Visa", 0,
            new PaymentOptionsModel
            {
                ImageUrl = @"https://w7.pngwing.com/pngs/20/987/png-transparent-logo-visa-credit-card-business-visa-text-trademark-payment.png",
                Title = "Visa",
                Description = "Payment using Visa service"
            });
        public static readonly PaymentOptionsEnum IBox = new("IBox terminal", 1,
            new PaymentOptionsModel
            {
                ImageUrl = @"https://w7.pngwing.com/pngs/790/495/png-transparent-chernihiv-payment-internet-privatbank-bank-computer-network-text-trademark.png",
                Title = "IBox terminal",
                Description = "Payment using IBox service"
            });
        public static readonly PaymentOptionsEnum Bank = new("Bank", 2,
            new PaymentOptionsModel
            {
                ImageUrl = @"https://thepage.fra1.digitaloceanspaces.com/live/media/86774/conversions/privatbank-r0-square_medium.jpg?v=1603812987",
                Title = "Bank",
                Description = "Payment using Bank service"
            });

        public PaymentOptionsModel Model { get; }
        public PaymentOptionsEnum(string name, int value, PaymentOptionsModel model) : base(name, value) 
        {
            this.Model = model;
        }
    }
}
