using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class Category 
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
