using Microsoft.AspNetCore.Identity;

namespace ecommerce.Models
{
	public class AppUser:IdentityUser
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public bool IsDeactive { get; set; }
	}
}
