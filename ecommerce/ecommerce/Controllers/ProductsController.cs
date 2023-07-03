using ecommerce.DAL;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Controllers
{
	public class ProductsController : Controller
	{
		private readonly AppDbContext _db;
        public ProductsController(AppDbContext db)
        {
			_db = db;	
        }
        public IActionResult Index()
		{
			return View();
		}


		public async Task<IActionResult> Detail(int? id)
		{
			ViewBag.Products = await _db.Products.Include(x=>x.Category).Take(4).ToListAsync();
			if (id == null)
			{
				return NotFound();
			}
			Product product = await _db.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
			if (product == null)
			{
				return BadRequest();

			}

			return View(product);

		}
	}
}
