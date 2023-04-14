using Ciber.Entity;
using Ciber.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ciber.Controllers
{
    public class NewsController : Controller
    {
        private readonly CiberDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public NewsController(CiberDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }
        public IActionResult Index()
        {
            var news = _context.News.ToList();
            return View(news);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(News request, IFormFile file)
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
                request.Image = fileName;
                _context.News.Add(request);
                _context.SaveChanges();
                return Redirect("/news");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> Update(News request, IFormFile file)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == request.Id);
            news.Title = request.Title;
            news.Description = request.Description;
            news.Author = request.Author;
            news.Content = request.Content;

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
                news.Image = fileName;
            }
            _context.News.Update(news);
            _context.SaveChanges();
            return Redirect("/news");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var news = await _context.News.FindAsync(id);
            var result = _context.News.Remove(news);
            return Json(new { status = 1, message = "Xóa thành công" });
        }
    }
}
