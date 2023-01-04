using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;
using TrainingWebsite.Data;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ICourse data;
        private readonly ApplicationDbContext db;

        public HomeController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ICourse _data, ApplicationDbContext _db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            data = _data;
            db = _db;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Profile()
        {
            var Id = _userManager.GetUserId(HttpContext.User);
            ViewBag.Id = Id;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile(string Id)
        {
            Id = _userManager.GetUserId(HttpContext.User);
            ViewBag.Id = Id;
            ViewBag.JobList = data.OccuptionDropDown();
            ViewBag.LevelList = data.LevelDropDown();
            var model = await db.Users.SingleOrDefaultAsync(x => x.Id.Contains(Id));
            var viewModel = new EditProfile
            {

                FullName = model.FullName,
                BirthDate = model.BirthDate,
                Address = model.Address,
                Phone = model.PhoneNumber,
                Email = model.Email,
                Image = model.Image,
                JobID = model.OccuptionID,
                LevelID = model.LevelID
            };
            if (viewModel == null)
                return NotFound();
            else return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(string Id, EditProfile user)
        {
            var adminId = _userManager.GetUserId(HttpContext.User);
            ViewBag.Id = adminId;
            ViewBag.JobList = data.OccuptionDropDown();
            ViewBag.LevelList = data.LevelDropDown();

            if (ModelState.IsValid)
            {
                var model = await db.Users.SingleOrDefaultAsync(m => m.Id == Id);

                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        model.Image = dataStream.ToArray();
                    }
                    await _userManager.UpdateAsync(model);
                }


                model.Id = user.Id;
                model.FullName = user.FullName;
                model.BirthDate = user.BirthDate;
                model.Address = user.Address;
                model.PhoneNumber = user.Phone;
                model.Email = user.Email;
                model.OccuptionID = user.JobID;
                model.LevelID = user.LevelID;
                await db.SaveChangesAsync();
                return RedirectToAction("Profile");
            }

            return View();

        }
    }
}
