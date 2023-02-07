using Ciber.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Ciber.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly CiberDbContext _context;
        private readonly ILogger _logger;
        public OrdersController(CiberDbContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var orders = _context.Orders
               .Include(x => x.Product).ThenInclude(x => x.Category)
               .Include(x => x.Customer);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "prod_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = _context.Orders
               .Where(x => x.Product.Name.Contains(searchString) || x.Customer.Name.Contains(searchString))
               .Include(x => x.Product).ThenInclude(x => x.Category)
               .Include(x => x.Customer);
            }
            switch (sortOrder)
            {
                case "prod_desc":
                    orders = orders.OrderBy(s => s.Product.Name)
                        .Include(x => x.Product).ThenInclude(x => x.Category)
                        .Include(x => x.Customer);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_context.Customers.ToList(), "Id", "Name");
            ViewBag.ProductId = new SelectList(_context.Products.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            // check avaiable product befor create order
            if (order != null && order.ProductId > 0)
            {
                var productOrder = _context.Products.FirstOrDefault(x => x.Id == order.ProductId);
                var avaiableProduct = _context.Products.Count(x => x.Id == order.ProductId && x.Quantity > order.Amount);
                if (avaiableProduct > 0)
                {
                    try
                    {
                        _context.Orders.Add(order);
                        var isAdded = _context.SaveChanges();
                        if (isAdded > 0)
                        {
                            productOrder.Quantity = productOrder.Quantity - order.Amount;
                            _context.Products.Update(productOrder);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error: Can't create new order, An error has occurred " + ex);
                    }
                }
                else
                {
                    _logger.LogError($"Error: Can't create new order, Product quantity is not enough");
                    return View("Error", "Error: Can't create new order, Product quantity is not enough");
                }
            }
            return View();
        }

        public IActionResult Error(string error)
        {
            return View(error);
        }
    }
}
