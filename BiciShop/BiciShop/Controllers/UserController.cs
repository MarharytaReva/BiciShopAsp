using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiciShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BiciShop.Models.ViewModels;

namespace BiciShop.Controllers
{
    [Authorize(Roles = "head")]
    public class UserController : Controller
    {
        readonly UserManager<IdentityUser> userManager;
        readonly RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
       
        public async Task<IActionResult> Users()
        {
            var exceptions = await userManager.GetUsersInRoleAsync("head");
            var users = userManager.Users.ToList().Except(exceptions);
            List<UserViewModel> model = new List<UserViewModel>();
            foreach(var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                string rolesStr = string.Join(' ', roles);
                model.Add(new UserViewModel()
                {
                     User = user,
                     Roles = rolesStr
                });
            }
            return View(model);
        }
       
        public async Task<IActionResult> Edit(string userId)
        {
            IdentityUser user = await userManager.FindByIdAsync(userId);
            if(user != null)
            {
                EditUserViewModel model = new EditUserViewModel()
                {
                    UserId = userId,
                    Email = user.Email,
                    UsersRoles = await userManager.GetRolesAsync(user),
                    AllRoles = roleManager.Roles.ToList()
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, List<string> roles)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    model.UsersRoles = await userManager.GetRolesAsync(user);
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    
                    List<string> addedRoles = roles.Except(model.UsersRoles).ToList();
                    List<string> deletedRoles = model.UsersRoles.Except(roles).ToList();
                    await userManager.AddToRolesAsync(user, addedRoles);
                    await userManager.RemoveFromRolesAsync(user, deletedRoles);
                    await userManager.UpdateAsync(user);
                    return RedirectToAction("Users");
                }
            }
           
            return View(model);
        }
        
        public IActionResult Create()
        {
            CreateUserViewModel model = new CreateUserViewModel()
            {
                AllRoles = roleManager.Roles.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model, List<string> roles)
        {
            model.AllRoles = roleManager.Roles.ToList();
            if (ModelState.IsValid)
            {
                IdentityUser newUser = new IdentityUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                };
                var result = await userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    IdentityUser createdUser = await userManager.FindByEmailAsync(newUser.Email);

                    await userManager.AddToRolesAsync(createdUser, roles);
                    await userManager.UpdateAsync(createdUser);
                    return RedirectToAction("Users", "User");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("Email", error.Description);
                    return View(model);
                }
            }
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            IdentityUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }

            return RedirectToAction("Users");
        }
    }
}
