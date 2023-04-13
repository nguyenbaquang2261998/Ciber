using Ciber.DTO;
using Ciber.Entity;
using Ciber.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ciber.Controllers
{
    public class CartController : Controller
    {
        private readonly CiberDbContext _context;
        public CartController(CiberDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Order> dataCart = JsonConvert.DeserializeObject<List<Order>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult>  Checkout()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Order> dataCart = JsonConvert.DeserializeObject<List<Order>>(cart);
                int total = 0;
                foreach (var item in dataCart)
                {
                    int price = item.Product.Price;
                    total = price * item.Amount;
                }
                ViewBag.Total = total;
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Payment(PackOrder packOrder)
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            List<Order> dataCart = JsonConvert.DeserializeObject<List<Order>>(cart);

            packOrder.Orders = dataCart;
            foreach (var item in packOrder.Orders)
            {
                item.Product = null;
                item.CustomerId = 1;
            }

            _context.PackOrders.Add(packOrder);
            await _context.SaveChangesAsync();

            // Sau khi thanh toán xong reset lại list sản phẩm trong giỏ hàng.
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(new List<Order>()));

            return Redirect("/cart/complete");
        }

        public IActionResult Complete()
        {
            return View();
        }

        public async Task<IActionResult> CheckingOrderByClient(int orderCode)
        {
            var orders = _context.PackOrders.FirstOrDefault(x => x.Id == orderCode);

            if (orders != null)
            {
                return View("TrackingSucces", orders);
            }
            return View("404");
        }
        public Product GetDetails(int id)
        {
            var product = _context.Products.Find(id);
            return product;
        }
        public List<Product> GetAllProduct()
        {
            var res = _context.Products.ToList();
            return res;
        }

        public IActionResult AddCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart == null)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == id);
                List<Order> listCart = new List<Order>()
               {
                   new Order
                   {
                       Product = product,
                       Amount = 1
                   }
               };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));

            }
            else
            {
                List<Order> dataCart = JsonConvert.DeserializeObject<List<Order>>(cart);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart[i].Amount++;
                        check = false;
                    }
                }
                if (check)
                {
                    dataCart.Add(new Order
                    {
                        Product = _context.Products.FirstOrDefault(x => x.Id == id),
                        Amount = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            }

            return Redirect("/Cart/Index");
        }

        [HttpPost]
        public IActionResult UpdateCart(int id, int quantity)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Order> dataCart = JsonConvert.DeserializeObject<List<Order>>(cart);
                if (quantity > 0)
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].Product.Id == id)
                        {
                            dataCart[i].Amount = quantity;
                        }
                    }


                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                }
                var cart2 = HttpContext.Session.GetString("cart");
                return Ok(quantity);
            }
            return BadRequest();

        }


        public IActionResult DeleteCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Order> dataCart = JsonConvert.DeserializeObject<List<Order>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                if (dataCart.Count == 0)
                {
                    return Redirect("/Home");
                }
                return Redirect("/Cart/Index");
            }
            return Redirect("/Home");
        }

        public IActionResult ProductNotFound()
        {
            return View();
        }
    }
}
