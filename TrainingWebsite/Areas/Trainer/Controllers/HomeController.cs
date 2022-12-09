using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;

namespace TrainingWebsite.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.userId = _userManager.GetUserId(HttpContext.User);
            return View();
        }
    }
}
