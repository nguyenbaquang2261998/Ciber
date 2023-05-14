using Ciber.Enums;
using Ciber.Model;
using System.ComponentModel.DataAnnotations;

namespace Ciber.Entity
{
    public class PackOrder
    {
        [Key]
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Address { get; set; }

        public int TotalMoney { get; set; }
        public DateTime OrderDate { get; set; }

        public List<Order> Orders { get; set; }

        public OrderStatus Status { get; set; }
    }
}
