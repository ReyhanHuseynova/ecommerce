﻿using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModels
{
    public class UpdateVM
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Role { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
