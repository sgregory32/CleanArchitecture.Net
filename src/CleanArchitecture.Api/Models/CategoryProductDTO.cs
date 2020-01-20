using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Api.Models
{
    /// <summary>
    /// Category DTO with ICollection of ProductCategoryDTO's which eliminates circular reference back to Product from Category. 
    /// </summary>
    public class CategoryProductDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<ProductCategoryDTO> Product { get; set; }
    }
}
