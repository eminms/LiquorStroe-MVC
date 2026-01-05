using Liquourstore.DAL;
using Liquourstore.Models;
using Liquourstore.Utilities.ImageUpload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Liquourstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        LiquorDbContext _context;
        IWebHostEnvironment _env;
        public ProductController(LiquorDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.Include(p => p.Categories).ToListAsync();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (product.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image is required");
                return View();
            }
            if (!product.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File type must be image");
                return View();
            }
            if (product.ImageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "Image size must be max 2MB");
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            string fileName = product.ImageFile.SaveImage(_env, "Uploads/Product");
            product.ImageUrl = fileName;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(p=>p.Id==id);
            if (product == null) return NotFound();
            product.ImageUrl.DeleteImage(_env, "Uploads/Product");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            Product existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null) return NotFound();
            if (product.ImageFile != null)
            {
                if (!product.ImageFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImageFile", "File type must be image");
                    return View(existingProduct);
                }
                if (product.ImageFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2MB");
                    return View(existingProduct);
                }
                existingProduct.ImageUrl.DeleteImage(_env, "Uploads/Product");
                string fileName = product.ImageFile.SaveImage(_env, "Uploads/Product");
                existingProduct.ImageUrl = fileName;
            }
            if (!ModelState.IsValid)
            {
                return View(existingProduct);
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Categories = product.Categories;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
