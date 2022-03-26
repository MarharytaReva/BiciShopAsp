using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BiciShop.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UsersRoles { get; set; }
        public EditUserViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UsersRoles = new List<string>();
        }
    }
}
