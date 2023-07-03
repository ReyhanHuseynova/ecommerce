using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Sale { get; set; }
        public bool IsDeactive { get; set; }
       
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
