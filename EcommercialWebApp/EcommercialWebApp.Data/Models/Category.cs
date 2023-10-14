using EcommercialWebApp.Data.Models.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommercialWebApp.Data.Models
{
    [Table("Categories")]
    public class Category : IEntityId, IAuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 6, ErrorMessage = "The {0} must be between {2} to {1}")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<ProductsInCategories> ProductsInCategories { get; set; }
    }
}
