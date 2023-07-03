using ecommerce.DAL;
using ecommerce.Helpers;

using ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Controllers
{
	public class CartController : Controller
	{
		private readonly AppDbContext _db;
		
		public CartController(AppDbContext db )
		{
			_db = db;
		

		}
		public IActionResult Index()
		{
			

			List<Cart> cart = _db.Carts.Include(x => x.Product).Where(x=>x.IsDeactive==false).ToList();
			return View(cart);
		}

		
		[HttpPost]
		public ActionResult Add(int productId, Cart cart)
		{

			if (User.Identity.IsAuthenticated)
			{
				Cart carts = _db.Carts.Where(x => x.Product.Id == productId).FirstOrDefault();
				_db.Carts.Add(cart);
				_db.SaveChanges();

			}
			else
			{
				return RedirectToAction("Login", "Account");
			}

			return RedirectToAction("Index");
		}

		public IActionResult Remove(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Cart cart= _db.Carts.FirstOrDefault(x => x.Id == id);	
			if(cart == null)
			{
				return BadRequest();
			}
			cart.IsDeactive = true;
			_db.SaveChanges();

			return RedirectToAction("Index");
		}


	}
}
