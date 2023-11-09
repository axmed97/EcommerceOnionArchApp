using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Fullname { get; set; }
        public List<Order> Orders { get; set; }
    }
}
