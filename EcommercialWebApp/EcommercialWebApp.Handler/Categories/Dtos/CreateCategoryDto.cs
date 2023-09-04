using System.ComponentModel.DataAnnotations;

namespace EcommercialWebApp.Handler.Categories.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(120, MinimumLength = 6, ErrorMessage = "The {0} must be between {2} to {1}")]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
