namespace Business.Models
{
    public class IBoxTransactionModel : PaymentModel
    {
        public string AccountNumber { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
