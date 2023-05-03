using Ciber.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ciber.Controllers
{
    public class HomeController : Controller
    {
        private readonly CiberDbContext _context;
        public HomeController(CiberDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Recruire()
        {
            return View();
        }

        public IActionResult News()
        {
            var news = _context.News.ToList();
            return View(news);
        }

        public IActionResult NewsDetail(int id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);
            return View(news);
        }

        public IActionResult Product(int id)
        {
            var products = _context.Products.Where(x => x.CategoryId == id)
                .Include(x => x.Category).ToList();
            return View(products);
        }

        public IActionResult Tracking()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Tracking(int id)
        {
            var packOrder = _context.PackOrders.FirstOrDefault(x => x.Id == id);
            return View(packOrder);
        }
    }
}
