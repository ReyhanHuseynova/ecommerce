using ecommerce.DAL;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecommerce.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminContactController : Controller
	{
		private readonly AppDbContext _db;
        public AdminContactController(AppDbContext db)
        {
			_db = db;
        }
        public   async Task<IActionResult> Index()
		{
			List<Contact> contacts= await _db.Contacts.ToListAsync();
			return View(contacts);
		}

		public async Task<IActionResult> Detail(int? id)
		{
			if(id == null)
			{
				return NotFound();
			}

			Contact contact = await _db.Contacts.FirstOrDefaultAsync(x=>x.Id==id);

			if(contact == null)
			{
				return NotFound();
			}

			
			return View(contact);
		}

		public async Task<IActionResult> Activity(int? id)
		{
			if(id == null)
			{
				return NotFound();
			}
			Contact dbContact= await _db.Contacts.FirstOrDefaultAsync(x=>x.Id==id);

            if (dbContact == null)
            {
				return BadRequest();
            }

			if(dbContact.Isdeactive) 
			{
			  dbContact.Isdeactive = false;
			}
			else
			{
				dbContact.Isdeactive = true;
			}

			await _db.SaveChangesAsync();
			return RedirectToAction("Index");	
        }
	}
}
