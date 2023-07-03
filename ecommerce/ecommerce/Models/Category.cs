using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
	public class Category
	{
        public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string TypeCategory { get; set; }
        public bool IsDeactive { get; set; }
        public List<Product> Products { get; set; }
    }
}
