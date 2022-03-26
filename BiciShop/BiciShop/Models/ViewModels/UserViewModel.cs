using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BiciShop.Models.ViewModels
{
    public class UserViewModel
    {
        public IdentityUser User { get; set; }
        public string Roles { get; set; }
    }
}
