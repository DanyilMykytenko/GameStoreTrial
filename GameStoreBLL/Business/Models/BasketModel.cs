namespace Business.Models
{
    public class BasketModel
    {
        public int Id { get; set; }
        public ICollection<OrderDetailsModel>? Details { get; set; }
        public virtual int? CustomerId { get; set; }
    }
}
