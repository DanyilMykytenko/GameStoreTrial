namespace Business.Models
{
    public class BankTransactionModel : PaymentModel
    {
        public int? OrderId { get; set; }
    }
}
