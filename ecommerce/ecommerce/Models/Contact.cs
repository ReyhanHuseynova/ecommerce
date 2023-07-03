using System;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
	public class Contact
	{
		public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        public string YourMessage { get; set; }
        public bool Isdeactive { get; set; }
    }
}
