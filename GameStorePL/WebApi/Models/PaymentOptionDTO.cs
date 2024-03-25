namespace WebApi.Models;

public class PaymentOptionDTO
{
    public string Method { get; set; }

    public VisaPaymentDTO? Model { get; set; }
}
