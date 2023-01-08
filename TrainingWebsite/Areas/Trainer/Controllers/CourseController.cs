using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;
using TrainingWebsite.Areas.Trainer.Models;
using TrainingWebsite.Data;
using TrainingWebsite.Models;

namespace TrainingWebsite.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    [Authorize(Roles = "Trainer")]

    public class CourseController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment WebHostEnvironment;
        public CourseController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext dataContext, IWebHostEnvironment WebHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            data = dataContext;
            this.WebHostEnvironment = WebHostEnvironment;


        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)
        {
            ModelState.Remove("id");
            var trainerId = _userManager.GetUserId(HttpContext.User);

            if (trainerId == null)
            {
                return RedirectToAction("Login", "Account", new { Areas = "Manager" });
            }
            else
            {
                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewData["CurrentFilter"] = searchString;
                var course = (from co in data.Courses
                              join s in data.Occuptions on co.IDJobPos equals s.OccuptionID
                              join u in data.Users on co.IDTrainer equals u.Id
                              where u.Id.Contains(trainerId)
                              select new ListCourseViewModel
                              {
                                  ID = co.courseID,
                                  MaKhoaHoc = co.MaKhoaHoc,
                                  Name = co.TenKhoaHoc,
                                  ThoiLuongKhoaHoc = co.ThoiLuongKhoaHoc,
                                  MucTieuKhoaHoc = co.MucTieuKhoaHoc,
                                  HinhThucDanhGia = co.HinhThucDanhGia,

                                  JobPosName = s.OccuptionName
                              });

                if (!String.IsNullOrEmpty(searchString))
                {
                    course = course.Where(s => s.Name.Contains(searchString) || s.ID.ToString().Contains(searchString) || s.MaKhoaHoc.Contains(searchString)
                    || s.JobPosName.Contains(searchString));
                }


                int pageSize = 3;
                return View(await PaginatedList<ListCourseViewModel>.CreateAsync(course.AsNoTracking(), pageNumber ?? 1, pageSize));
            }


        }
        private void OccuptionDropDownList()
        {
            var OccuptionList = (from s in data.Occuptions
                                 select new SelectListItem()
                                 {
                                     Text = s.OccuptionName,
                                     Value = s.OccuptionID.ToString()


                                 }).ToList();
            OccuptionList.Insert(0, new SelectListItem()
            {
                Text = "-----Select-----",
                Value = string.Empty
            });
            ViewData["ListOfOccuption"] = OccuptionList;
        }
        [HttpGet]
        public IActionResult AddCourse()
        {

            OccuptionDropDownList();
            var trainerId = _userManager.GetUserId(HttpContext.User);
            ViewBag.TrainerId = trainerId;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse(CreateCourseViewModel model)
        {
            var trainerId = _userManager.GetUserId(HttpContext.User);

            if (ModelState.IsValid)
            {
                string uniuefilename = string.Empty;
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;

                        var uploads = Path.Combine(WebHostEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            // var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                uniuefilename = fileName;
                            }

                        }
                    }
                }
                var course = new Course()
                {
                    MaKhoaHoc = model.MaKhoaHoc,
                    TenKhoaHoc = model.TenKhoaHoc,
                    ThoiLuongKhoaHoc = model.ThoiLuongKhoaHoc,
                    MucTieuKhoaHoc = model.MucTieuKhoaHoc,
                    HinhThucDanhGia = model.HinhThucDanhGia,

                    IDTrainer = trainerId,
                    ImageTrainer = uniuefilename,
                    IDJobPos = model.IDJobPos

                };
                data.Courses.Add(course);
                await data.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            OccuptionDropDownList();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditCourse(int id)
        {
            var trainerId = _userManager.GetUserId(HttpContext.User);
            ViewData["TrainerId"] = trainerId;

            var course = await data.Courses.Where(x => x.courseID == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return NotFound();
            }
            OccuptionDropDownList();

            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(Course _course)
        {
            var trainerId = _userManager.GetUserId(HttpContext.User);
            ViewData["TrainerId"] = trainerId;
            if (ModelState.IsValid)
            {
                Course course = data.Courses.Where(p => p.courseID == _course.courseID).FirstOrDefault();
                string uniuefilename = course.ImageTrainer;
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;

                        var uploads = Path.Combine(WebHostEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            // var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                uniuefilename = fileName;
                            }

                        }
                    }
                }
                course.MaKhoaHoc = _course.MaKhoaHoc;
                course.TenKhoaHoc = _course.TenKhoaHoc;
                course.ThoiLuongKhoaHoc = _course.ThoiLuongKhoaHoc;
                course.MucTieuKhoaHoc = _course.MucTieuKhoaHoc;
                course.HinhThucDanhGia = _course.HinhThucDanhGia;
                course.IDTrainer = trainerId;
                course.ImageTrainer = uniuefilename;
                course.IDJobPos = _course.IDJobPos;
               
                await data.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            OccuptionDropDownList();
            return View();
        }
        
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await data.Courses.SingleOrDefaultAsync(m => m.courseID == id);
            //var courseTrainee = await data.CourseTrainees.SingleOrDefaultAsync(m => m.CourseID == id);

            data.Courses.Remove(course);
            
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
