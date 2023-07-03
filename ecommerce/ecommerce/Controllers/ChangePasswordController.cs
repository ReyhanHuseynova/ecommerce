using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class ChangePasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
