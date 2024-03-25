namespace Business.PaymentStrategies
{
    public interface IPaymentFactory
    {
        public Task<IStrategy> Create<TModel>(string option);
    }
}
