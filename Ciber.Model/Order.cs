using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciber.Model
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public int Amount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
