using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public string Address { get; set; }
        //public List<Product> Products { get; set; }
        //public Guid CustomerId { get; set; }
        //public Customer Customer { get; set; }
        public Basket Basket { get; set; }
    }
}
