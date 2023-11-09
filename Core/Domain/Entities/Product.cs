using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<Order> Orders { get; set; }
    }
}
