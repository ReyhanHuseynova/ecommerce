using ecommerce.DAL;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecommerce.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
            _db = db;
        }
		public async Task<IActionResult> Index()
		{
			
			return View();
		}
		public async Task<IActionResult> Create()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(Contact contact)
		{
            if(!ModelState.IsValid)
            {
               return View();
            }
            await _db.Contacts.AddAsync(contact);
            await _db.SaveChangesAsync();
			return View();
		}

	}
}
