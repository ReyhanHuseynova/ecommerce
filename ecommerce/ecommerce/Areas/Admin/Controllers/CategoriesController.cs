using ecommerce.DAL;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ecommerce.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoriesController : Controller
	{
		private readonly AppDbContext _db;
        public CategoriesController(AppDbContext db)
        {
			_db = db;
        }
        public async Task<IActionResult> Index()
		{
			List<Category> categories= await _db.Categories.ToListAsync();
			return View(categories);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category category)
		{
			if(!ModelState.IsValid)
			{
				return View();
			}
			
			
			
			await _db.Categories.AddAsync(category);
			await _db.SaveChangesAsync();	
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Update(int? id)
		{
			if(id == null) 
			{ 
			 return NotFound();
			}

			Category dbCategory =await _db.Categories.FirstOrDefaultAsync(x=>x.Id==id);

			if(dbCategory == null)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return View();
			}
			return View(dbCategory);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(int? id, Category category)
		{
			if (id == null)
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
				return View();
			}

			Category dbCategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);

			if (dbCategory == null)
			{
				return BadRequest();
			}

			dbCategory.Name = category.Name;
			dbCategory.TypeCategory = category.TypeCategory;
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Activity(int?id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Category dbCategory = await _db.Categories.FirstOrDefaultAsync(x=>x.Id==id);

			if(dbCategory == null)
			{
				return BadRequest();
			}

			if(dbCategory.IsDeactive)
			{
				dbCategory.IsDeactive = false;
			}
			else
			{
				dbCategory.IsDeactive = true;
			}

			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
	