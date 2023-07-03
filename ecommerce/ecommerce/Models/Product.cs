using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
	public class Product
	{
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        public Category  Category { get; set; }
        public int  CategoryId { get; set; }
        public bool IsDeactive { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public List<Stock> Stocks { get; set; }
      
        public List<Cart> Carts { get; set; }

    }
}
