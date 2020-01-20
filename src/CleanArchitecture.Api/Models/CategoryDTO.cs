using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Api.Models
{
    public class CategoryDTO
    {
        /// <summary>
        /// Category DTO without Product reference which eliminates circular reference.
        /// </summary>
        /// 
        public int? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
