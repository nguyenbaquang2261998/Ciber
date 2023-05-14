using Ciber.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Ciber.DTO;
using Ciber.Model;

namespace Ciber.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly CiberDbContext _context;

        public AdminController(ILogger<AdminController> logger, CiberDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var res = new ReportDto();
            var orders = _context.PackOrders.Where(x => x.OrderDate.Date == DateTime.Now.Date).ToList();
            res.Orders = orders.Count();
            var total = 0;
            foreach (var item in orders)
            {
                total += item.TotalMoney;
            }
            res.TotalMoney = total;
            return View(res);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}