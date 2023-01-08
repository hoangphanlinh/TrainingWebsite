using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Admin.Models;
using TrainingWebsite.Areas.Identity.Data;
using TrainingWebsite.Data;
using TrainingWebsite.Models;
using TrainingWebsite.ViewModels;
using X.PagedList;

namespace TrainingWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TraineeRegisterList : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dataContext;
        private readonly ICourse _course;

        public TraineeRegisterList(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext dataContext, ICourse course)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.dataContext = dataContext;
            _course = course;
        }
        [HttpGet]
        public IActionResult getEmploeeList(int? size, int? page, string txtSearch, int JobPosID = 0, int LevelID = 0, int ApartID = 0)
        {
            ViewBag.txtSearch = txtSearch;
            ViewBag.JobPosList = _course.OccuptionDropDown();
            ViewBag.JobPosID = JobPosID;
            ViewBag.LevelList = _course.LevelDropDown();
            ViewBag.LevelID = LevelID;
            ViewBag.ApartList = _course.ApartmentDropDown();
            ViewBag.ApartID = ApartID;

            var data = _course.getEmployeeList();

            if (!string.IsNullOrEmpty(txtSearch))
            {
                data = data.Where(x => x.Email.Contains(txtSearch));
            }
            if (JobPosID != 0)
            {
                data = data.Where(c => c.OccuptionID == JobPosID);
            }

            if (LevelID != 0)
            {
                data = data.Where(a => a.LevelID == LevelID);
            }
            if (ApartID != 0)
            {
                data = data.Where(a => a.ApartID == ApartID);
            }
            ViewBag.Page = page;
            // 3.1. Tạo danh sách chọn số trang
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "4", Value = "4" });
            items.Add(new SelectListItem { Text = "8", Value = "8" });
            items.Add(new SelectListItem { Text = "12", Value = "12" });
            items.Add(new SelectListItem { Text = "16", Value = "16" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });

            // 3.2. Thiết lập số trang đang chọn vào danh sách List<SelectListItem> items
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.Size = items;
            ViewBag.CurrentSize = size;
            // 3.3. Nếu page = null thì đặt lại là 1.
            page = page ?? 1; //if (page == null) page = 1;

            // 3.4. Tạo kích thước trang (pageSize), mặc định là 5.
            int pageSize = (size ?? 5);

            ViewBag.pageSize = pageSize;

            // 3.5. Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 3.6 Lấy tổng số record chia cho kích thước để biết bao nhiêu trang
            int checkTotal = (int)(data.ToList().Count / pageSize) + 1;
            // Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            // 4. Trả kết quả về Views
            return View(data.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {

                    return RedirectToAction("ListUsers");

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Enroll(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trainee = await dataContext.Users.FindAsync(id);
            if(trainee == null)
            {
                return NotFound();
            }
            var result = from c in dataContext.Courses
                         select new
                         {
                             c.courseID,
                             c.TenKhoaHoc,
                             Checked = ((from ab in dataContext.CourseTrainees
                                         where (ab.CourseID == c.courseID) && (ab.TraineeID == id)
                                         select ab).Count() > 0)
                         };
            var MyViewModel = new TraineeCourseViewModel();
            MyViewModel.TraineeID = id;

            var checkBoxList = new List<checkBoxViewModel>();
            foreach(var item in result)
            {
                checkBoxList.Add(new checkBoxViewModel
                {
                    courseID = item.courseID,
                    courseName = item.TenKhoaHoc,
                    IsSelected = item.Checked
                });
            }
            MyViewModel.Courses = checkBoxList;
            ViewBag.TraineeID = id;
            ViewBag.LevelList = _course.LevelStringDropDown();
            return View(MyViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(TraineeCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MyUser = dataContext.Users.Find(model.TraineeID);
                   

                    foreach(var item in dataContext.CourseTrainees)
                    {
                        if(item.TraineeID == model.TraineeID)
                        {
                            dataContext.Remove(item);
                        }
                    }
                    foreach(var item in model.Courses)
                    {
                        if (item.IsSelected)
                        {
                            dataContext.CourseTrainees.Add(new TraineeCourse()
                            {
                                CourseID = item.courseID,
                                TraineeID = model.TraineeID,
                                EnrollDate = model.ErollDate,
                                Status = model.Status,
                                TraineeLevel = model.TraineeLevel,
                                Title = model.Title
                            });
                        }
                    }
                    await dataContext.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(model.TraineeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                   
                }
                return RedirectToAction("Details",new { id = model.TraineeID});
            }
            ViewBag.LevelList = _course.LevelStringDropDown();
            return View(model);
        }
        private bool UserExists(string id)
        {
            return dataContext.Users.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trainee = dataContext.Users.FirstOrDefault(m=>m.Id == id);
            if(trainee == null)
            {
                return NotFound();
            }
            var myViewModel = new TraineeCourseViewModel();
            myViewModel.Email = trainee.Email;
            myViewModel.CourseList = (from c in dataContext.Courses
                                      join uc in dataContext.CourseTrainees on c.courseID equals uc.CourseID
                                      join cl in dataContext.CourseClassrooms on c.courseID equals cl.courseID
                                      where uc.TraineeID == id
                                      select new CourseDetails
                                      {
                                          courseID = uc.CourseID,
                                          TenKhoaHoc = c.TenKhoaHoc,
                                          classID = cl.classID
                                      }
                                     ).ToList();
            return View(myViewModel);
        }

    }
}
