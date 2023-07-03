using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Stock
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int StockOut { get; set; }
        public bool IsDeactive { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
