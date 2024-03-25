using Business.Models;

namespace Business.PaymentStrategies
{
    public class PaymentContext<TPayment> where TPayment : PaymentModel
    {
        public IStrategy ContextStrategy { get; set; }
        public TPayment Option { get; set; }

        public PaymentContext(IStrategy strategy, TPayment option)
        {
            ContextStrategy = strategy;
            Option = option;
        }

        public async Task ExecuteTransaction()
        {
            await ContextStrategy.MakeTransaction(Option);
        }
    }
}
