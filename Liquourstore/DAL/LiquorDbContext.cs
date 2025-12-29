using Liquourstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Liquourstore.DAL
{
    public class LiquorDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public LiquorDbContext(DbContextOptions<LiquorDbContext> options) : base(options) {}

    }
}
