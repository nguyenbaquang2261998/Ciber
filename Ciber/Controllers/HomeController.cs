using Microsoft.AspNetCore.Mvc;

namespace Ciber.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
