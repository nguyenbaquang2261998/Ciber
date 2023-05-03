using Ciber.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ciber.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly CiberDbContext _context;
        private readonly ILogger _logger;
        public CategoriesController(CiberDbContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var res = _context.Categories.OrderByDescending(x => x.Id).ToList();
            return View(res);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category categories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categories);
                await _context.SaveChangesAsync();
                return Redirect("/Categories/Index");
            }
            return View(categories);
        }

        public async Task<IActionResult> Update(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }

        
        [HttpPost]
        public async Task<IActionResult> Update(Category categories)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Categories.Update(categories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(categories.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            var result = _context.Categories.Remove(category);
            _context.SaveChanges();
            return Json(new { status = 1, message = "Xóa thành công" });
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
