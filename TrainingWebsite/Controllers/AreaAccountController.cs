using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;
using TrainingWebsite.Data;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Controllers
{
    [AllowAnonymous]
    public class AreaAccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext dataContext;

        public AreaAccountController(SignInManager<ApplicationUser> _signInManager, RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager, ApplicationDbContext _dataContext)
        {

            this.signInManager = _signInManager;
            roleManager = _roleManager;
            userManager = _userManager;
            dataContext = _dataContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);
                var userID = dataContext.Users.Where(m => m.Email.Contains(model.Email)).FirstOrDefault().Id;
                if (result.Succeeded)
                {
                    var roleID = dataContext.UserRoles.Where(m => m.UserId == userID).FirstOrDefault().RoleId;
                    if(roleManager.FindByIdAsync(roleID).Result.Name == "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if(roleManager.FindByIdAsync(roleID).Result.Name == "Manager")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Manager" });
                    }
                    else if (roleManager.FindByIdAsync(roleID).Result.Name == "Trainer")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Trainer" });
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
