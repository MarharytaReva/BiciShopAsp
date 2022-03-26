using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BiciShop.Models.ViewModels;
using BiciShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BiciShop.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<IdentityUser> userManager;
        readonly SignInManager<IdentityUser> signInManager;
        
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Settings()
        {
            string userId = userManager.GetUserId(User);
            return View("Settings", userId);
        }
        public async Task<IActionResult> ChangePassword(string userId, string actionName, string controllerName)
        {
            IdentityUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                ChangePasswordViewModel model = new ChangePasswordViewModel()
                {
                    ActionName = actionName,
                    ControllerName = controllerName,
                    Email = user.Email,
                    UserId = userId
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    var res = await userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                    if (res.Succeeded)
                        return RedirectToAction(model.ActionName, model.ControllerName);
                }
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                     return RedirectToAction("Index", "Home");
                }
                else
                {
                     ModelState.AddModelError("Email", "Email or password is not correct");
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {
            if(ModelState.IsValid)
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
                    
                    await userManager.AddToRoleAsync(createdUser, "user");
                    await userManager.UpdateAsync(createdUser);
                    return RedirectToAction("Login", "Account");
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
        public IActionResult Registration()
        {
            return View();
        }
    }
}
