namespace Data.Entities
{
    public class Order : BaseEntity
    {
        public ICollection<OrderDetails>? Details { get; set; }
        public bool? isPaid { get; set; }
        public DateTime? OrderDate { get; set; }
        public virtual int? CustomerId { get; set; }

    }
}
