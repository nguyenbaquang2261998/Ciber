using Ciber.DTO;
using Ciber.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ciber.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CiberDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public ProductsController(CiberDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .OrderByDescending(x => x.Id)
                .Include(x => x.Category)
                .ToList();

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var products = _context.Products.Where(x => x.Id == id);
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.ListCategories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile file)
        {
            if (file.Length > 0)
            {
                string fileName = file.FileName;
                //Get the extension of the file
                string extension = Path.GetExtension(fileName);
                //check the file extension as png
                if (extension == ".png" || extension == ".jpg")
                {
                    //set the path where file will be copied
                    string wwwPath = _environment.WebRootPath;
                    string filePath = wwwPath + "\\upload";
                    //copy the file to the path
                    using (var fileStream = new FileStream(
                        Path.Combine(filePath, fileName),
                                       FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                product.Image = fileName;
                _context.Products.Add(product);
                _context.SaveChanges();
                return Redirect("/products");
            }
            return View();
        }

        public IActionResult Update(int id)
        {
            ViewBag.ListCategories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            var products = _context.Products.FirstOrDefault(x => x.Id == id);
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product request, IFormFile file)
        {
            var products = _context.Products.FirstOrDefault(x => x.Id == request.Id);
            products.Name = request.Name;
            products.Price = request.Price;
            products.Description = request.Description;
            products.CategoryId = request.CategoryId;
            products.Quantity = request.Quantity;

            if (file != null && file.Length > 0)
            {
                string fileName = file.FileName;
                //Get the extension of the file
                string extension = Path.GetExtension(fileName);
                //check the file extension as png
                if (extension == ".png" || extension == ".jpg")
                {
                    //set the path where file will be copied
                    string wwwPath = _environment.WebRootPath;
                    string filePath = wwwPath + "\\upload";
                    //copy the file to the path
                    using (var fileStream = new FileStream(
                        Path.Combine(filePath, fileName),
                                       FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                products.Image = fileName;
            }
            _context.Products.Update(products);
            _context.SaveChanges();
            return Redirect("/products");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var result = _context.Products.Remove(product);
            return Json(new { status = 1, message = "Xóa thành công" });
        }
    }
}
