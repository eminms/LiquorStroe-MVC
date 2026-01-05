using Liquourstore.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Liquourstore.Models
{
    public class Product:BaseEntity
    {
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }


        [Required(ErrorMessage ="Name is required")]
        [MaxLength(100,ErrorMessage ="Name cannot exceed 100 characters"),
         MinLength(2,ErrorMessage ="Name must be at least 2 characters long")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description is required")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters"),
         MinLength(10, ErrorMessage = "Description must be at least 10 characters long")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Price is required")]
        [Range(0.1, 10000.00, ErrorMessage = "Price must be between 0.1 and 10,000.00")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Categories are required")]
        [MinLength(1, ErrorMessage = "At least one category must be selected"),
         MaxLength(5, ErrorMessage = "A maximum of 5 categories can be selected")]
        public List<Category> Categories { get; set; }

    }
}
