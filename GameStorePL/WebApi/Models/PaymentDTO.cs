namespace WebApi.Models;

public class PaymentDTO
{
    public double TransactionAmount { get; set; }

    public string AccountNumber { get; set; }

    public string InvoiceNumber { get; set; }

    public string CardholderName { get; set; }

    public string CardNumber { get; set; }

    public int ExpirationMonth { get; set; }

    public int Cvv2 { get; set; }

    public int ExpirationYear { get; set; }
}
