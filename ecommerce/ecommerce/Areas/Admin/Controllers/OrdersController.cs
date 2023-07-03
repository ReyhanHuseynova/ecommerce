using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrdersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
