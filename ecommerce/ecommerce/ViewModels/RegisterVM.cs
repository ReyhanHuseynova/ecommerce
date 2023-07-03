﻿using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModels
{
	public class RegisterVM
	{

		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
	
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]

		public string Username { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password")]

		public string CheckPassword { get; set; }
	}
}
