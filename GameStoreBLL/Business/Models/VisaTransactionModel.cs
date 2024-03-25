namespace Business.Models
{
    public class VisaTransactionModel : PaymentModel
    {
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int CVV { get; set; }
        public int ExpirationYear { get; set; }
    }
}
