using ecommerce.DAL;

using ecommerce.Models;
using hospital.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductsController : Controller
	{
		private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ProductsController(AppDbContext db, IWebHostEnvironment env)
        {
			_db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
		{
			List<Product> products = await _db.Products.Include(x=>x.Category).ToListAsync();
            
			return View(products);
		}

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _db.Categories.Where(x=>x.IsDeactive==false).ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, int categoryId)
        {
            
            ViewBag.Categories = await _db.Categories.Where(x => x.IsDeactive == false).ToListAsync();
           

            if (product.Photo == null)
            {
                ModelState.AddModelError("Photo", "Select photo");
                return View();
            }
            if (!product.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Select photo format");
                return View();
            }
            if (product.Photo.IsOlder2Mb())
            {
                ModelState.AddModelError("Photo", "Max 2Mb");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img", "product");
            product.Image = await product.Photo.SaveImageAsync(folder);

            product.CategoryId = categoryId;
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
             return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _db.Categories.Where(x => x.IsDeactive == false).ToListAsync();
            if (id == null)
            {
                return NotFound();
            }

            Product dbProduct = await _db.Products.Include(x=>x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (dbProduct == null)
            {
                return BadRequest();
            }
            return View(dbProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Product product, int categoryId)
        {
            ViewBag.Categories = await _db.Categories.Where(x => x.IsDeactive == false).ToListAsync();
            
            if (id == null)
            {
                return NotFound();
            }

            Product dbProduct =  await _db.Products.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id==id);
            if(dbProduct == null)
            {
                return BadRequest();
            }

            if (product.Photo != null)
            {
                if (!product.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo format");
                    return View(dbProduct);
                }
                if (product.Photo.IsOlder2Mb())
                {
                    ModelState.AddModelError("Photo", "Max 2Mb");
                    return View(dbProduct);
                }
                string folder = Path.Combine(_env.WebRootPath, "img", "product");
                string path = Path.Combine(folder, dbProduct.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbProduct.Image = await product.Photo.SaveImageAsync(folder);
            }


            dbProduct.CategoryId = categoryId;
            dbProduct.Price = product.Price;
            dbProduct.ProductName = product.ProductName;
            dbProduct.Description = product.Description;
           
           
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product dbProduct= await _db.Products.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbProduct == null)
            {
                return BadRequest();
            }
            if(dbProduct.IsDeactive)
            {
                dbProduct.IsDeactive = false;
            }
            else
            {
                dbProduct.IsDeactive=true;
            }



            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = await _db.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if(product == null)
            {
                return BadRequest();

            }
           
            return View(product);

        }
    }
}
