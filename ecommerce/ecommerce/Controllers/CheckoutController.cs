using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
