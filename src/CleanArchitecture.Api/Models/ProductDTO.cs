using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Api.Models
{
    public class ProductDTO 
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public CategoryDTO Category { get; set; }
    }
}
