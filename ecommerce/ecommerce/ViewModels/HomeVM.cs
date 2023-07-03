using ecommerce.Models;
using System.Collections.Generic;

namespace ecommerce.ViewModels
{
	public class HomeVM
	{
        public List<Slider> Sliders { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        
    }
}
