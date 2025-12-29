using Liquourstore.Models.Common;
namespace Liquourstore.Models
{
    public class Product:BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Category> Categories { get; set; }

    }
}
