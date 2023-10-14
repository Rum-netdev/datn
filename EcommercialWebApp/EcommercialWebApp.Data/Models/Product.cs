using EcommercialWebApp.Data.Models.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommercialWebApp.Data.Models
{
    [Table("Products")]
    public class Product : IEntityId, IAuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255, MinimumLength = 6, ErrorMessage = "The {0} must be between {2} to {1}")]
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<ProductsInCategories> ProductsInCategories { get; set; }
    }
}
