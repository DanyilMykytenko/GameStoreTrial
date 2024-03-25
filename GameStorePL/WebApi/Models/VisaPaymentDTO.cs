namespace WebApi.Models;

public class VisaPaymentDTO
{
    public double TransactionAmount { get; set; }

    public string Holder { get; set; }

    public string CardNumber { get; set; }

    public int MonthExpire { get; set; }

    public int Cvv2 { get; set; }

    public int YearExpire { get; set; }
}
