using ecommerce.DAL;
using ecommerce.Helpers;
using ecommerce.Models;
using ecommerce.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Controllers
{
    public class ShopsController : Controller
    {
        private readonly AppDbContext _db;
		
		public ShopsController(AppDbContext db)
        {
            _db = db;
		
        }
        public async Task<IActionResult> Index()
        {


			ViewBag.ProductCount = await _db.Products.CountAsync();
			HomeVM homeVM = new HomeVM
			{
				Products = await _db.Products.Take(3).ToListAsync(),
				Categories = await _db.Categories.ToListAsync(),
			};
			return View(homeVM);
        }

		#region Ajax
		public async Task<IActionResult> LoadMore(int skip)
		{
			if (_db.Products.Count() <= skip)
			{
				return Content("Qadagandir");
			}

			List<Product> products = await _db.Products.Include(x => x.Category).Skip(skip).Take(3).ToListAsync();
			return PartialView("_LoadMorePartial", products);
		}
		#endregion

		public IActionResult List(int? id)
		{
			
			List<Product> products= _db.Products.Include(x=>x.Category).Where(x=>x.CategoryId==id).ToList();
			
			return View(products);
		}

	}
}
