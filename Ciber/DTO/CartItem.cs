using Ciber.Model;

namespace Ciber.DTO
{
    public class CartItem
    {
        public Product Products { get; set; }
        public int Quantity { set; get; }
        public int Count { get; set; }
    }
}
