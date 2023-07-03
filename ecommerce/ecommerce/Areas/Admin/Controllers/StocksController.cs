using ecommerce.DAL;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecommerce.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class StocksController : Controller
	{
		private readonly AppDbContext _db;
		public StocksController(AppDbContext db)
		{
			_db = db;
		}
		public async Task<IActionResult> Index()
		{
			List<Stock> stocks = await _db.Stocks.Include(x => x.Product).ThenInclude(x => x.Category).ToListAsync();
			return View(stocks);
		}


		public async Task<IActionResult> Create()
		{
			ViewBag.Product = await _db.Products.ToListAsync();
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Stock stock, int pname)
		{




			if (!ModelState.IsValid)
			{
				return View();
			}

			ViewBag.Product = await _db.Products.ToListAsync();
			stock.ProductId = pname;
			await _db.Stocks.AddAsync(stock);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Update(int? id)
		{
			ViewBag.Product = await _db.Products.ToListAsync();
			if (id == null)
			{
				return NotFound();
			}

			Stock dbStock = await _db.Stocks.FirstOrDefaultAsync(x => x.Id == id);

			if (dbStock == null)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return View();
			}
			return View(dbStock);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(int? id, Stock stock, int pname)
		{
			ViewBag.Product = await _db.Products.ToListAsync();
			if (id == null)
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
				return View();
			}

			Stock dbStock = await _db.Stocks.FirstOrDefaultAsync(x => x.Id == id);

			if (dbStock == null)
			{
				return BadRequest();
			}

			dbStock.Quantity = stock.Quantity;
			dbStock.StockOut = stock.StockOut;
			dbStock.ProductId = pname;
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Activity(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Stock dbStock = await _db.Stocks.FirstOrDefaultAsync(x => x.Id == id);

			if (dbStock == null)
			{
				return BadRequest();
			}

			if (dbStock.IsDeactive)
			{
				dbStock.IsDeactive = false;
			}
			else
			{
				dbStock.IsDeactive = true;
			}

			await _db.SaveChangesAsync();
			return RedirectToAction("Index");


		}

	}
}
