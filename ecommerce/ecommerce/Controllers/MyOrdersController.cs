using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class MyOrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
