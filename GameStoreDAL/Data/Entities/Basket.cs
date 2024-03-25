namespace Data.Entities
{
    public class Basket : BaseEntity
    {
        public ICollection<OrderDetails>? Details { get; set; }
        public virtual int? CustomerId { get; set; }
    }
}
