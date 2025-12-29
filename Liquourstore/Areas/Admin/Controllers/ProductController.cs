using Liquourstore.DAL;
using Liquourstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Liquourstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        LiquorDbContext _context;
        public ProductController(LiquorDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.Include(p => p.Categories).ToListAsync();
            return View(products);
        }
    }
}
