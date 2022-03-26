using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Old password is required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "You should confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}
