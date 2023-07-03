using ecommerce.ViewModels;

namespace ecommerce.Models
{
	public class Order
	{
        public int Id { get; set; }
        public Cart Cart { get; set; }
        public int CartId { get; set; }

        public UserVM UserVM { get; set; }
        public int UserVMId { get; set; }
    }
}
