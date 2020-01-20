using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Api.Models
{
    public class ProductCategoryDTO
    {
        /// <summary>
        /// Product DTO without Category object which eliminates circular reference.
        /// </summary>
        /// 
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
