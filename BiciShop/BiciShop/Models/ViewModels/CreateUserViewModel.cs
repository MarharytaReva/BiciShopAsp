using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "You should confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm { get; set; }
        [Required(ErrorMessage = "You should select role")]
        public List<IdentityRole> AllRoles { get; set; } = new List<IdentityRole>();
    }
}
