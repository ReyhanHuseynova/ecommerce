using ecommerce.DAL;
using ecommerce.Models;
using ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM= new HomeVM
            {
                Sliders=  await _db.Sliders.ToListAsync(),
                Products=  await _db.Products.Include(x=>x.Category).Take(8).ToListAsync(),
              
                
            };
            ViewBag.Collection = await _db.Products.Include(x=>x.Category).Take(4).ToListAsync(); 
            return View(homeVM);
        }

       

       
        public IActionResult Error()
        {
            return View();
        }
    }
}
