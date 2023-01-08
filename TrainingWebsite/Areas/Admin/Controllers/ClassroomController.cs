using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
using TrainingWebsite.Data;
using TrainingWebsite.Models;
using TrainingWebsite.ViewModels;
using X.PagedList;

namespace TrainingWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ClassroomController : Controller
    {
        private readonly ICourse data;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly ApplicationDbContext dbContext;
        public ClassroomController(ICourse _data, UserManager<ApplicationUser> userManager,
            IWebHostEnvironment _WebHostEnvironment, ApplicationDbContext _dbContext)
        {
            data = _data;
            _userManager = userManager;
            WebHostEnvironment = _WebHostEnvironment;
            dbContext = _dbContext;
        }
        public IActionResult Index(int? page, int? size, string className)
        {
            var Id = _userManager.GetUserId(HttpContext.User);
            ViewBag.Id = Id;
            ViewBag.className = className;
            var result = data.getListClassroom(Id);
            if (!string.IsNullOrEmpty(className))
            {
                result = result.Where(x => x.Name.ToUpper().Contains(className.ToUpper()));
            }
            ViewBag.Page = page;
            //3.1.Tạo danh sách chọn số trang
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "4", Value = "4" });
            items.Add(new SelectListItem { Text = "8", Value = "8" });
            items.Add(new SelectListItem { Text = "12", Value = "12" });
            items.Add(new SelectListItem { Text = "16", Value = "16" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });

            //3.2.Thiết lập số trang đang chọn vào danh sách List<SelectListItem> items
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.Size = items;
            ViewBag.CurrentSize = size;
            //3.3.Nếu page = null thì đặt lại là 1.
          page = page ?? 1; //if (page == null) page = 1;

            //3.4.Tạo kích thước trang(pageSize), mặc định là 5.
            int pageSize = (size ?? 5);

            ViewBag.pageSize = pageSize;

            //3.5.Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            //3.6 Lấy tổng số record chia cho kích thước để biết bao nhiêu trang
            int checkTotal = (int)(result.ToList().Count / pageSize) + 1;
            //Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            //4.Trả kết quả về Views
            return View(result.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult createClassroom()
        {
            var Id = _userManager.GetUserId(HttpContext.User);
            ViewData["Id"] = Id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult createClassroom(createClassViewModel model, Classroom classroom)
        {
            var Id = _userManager.GetUserId(HttpContext.User);
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
                                file.CopyToAsync(fileStream);
                                uniuefilename = fileName;
                            }

                        }
                    }
                }
                classroom.Name = model.Name;
                classroom.startDate = model.NgayBatDau;
                classroom.endDate = model.NgayKetThuc;
                classroom.AdminID = Id;
                classroom.Image = uniuefilename;
                dbContext.Classrooms.Add(classroom);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Classroom");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditCourseIntoClass(int? id)
        {
            var ID = _userManager.GetUserId(HttpContext.User);
            if (id == null)
            {
                return NotFound();
            }
            var classroom = await dbContext.Classrooms.FindAsync(id);
            if(classroom == null)
            {
                return NotFound();
            }
            var result = from c in dbContext.Courses
                         select new
                         {
                             c.courseID,
                             c.TenKhoaHoc,
                             Checked = ((from ab in dbContext.CourseClassrooms
                                         where (ab.courseID == c.courseID) && (ab.classID == id)
                                         select ab).Count() > 0
                        )
                         };
            var MyViewModel = new ClassesViewModel();
            MyViewModel.classID = id.Value;
            MyViewModel.Name = classroom.Name;
            MyViewModel.startDate = classroom.startDate;
            MyViewModel.endDate = classroom.endDate;
            MyViewModel.AdminID = classroom.AdminID;
            MyViewModel.Image = classroom.Image;
            var checkListBox = new List<checkBoxViewModel>();
            foreach (var item in result)
            {
                checkListBox.Add(new checkBoxViewModel
                {
                    courseID = item.courseID,
                    courseName = item.TenKhoaHoc,
                    IsSelected = item.Checked
                });
            }
            MyViewModel.Courses = checkListBox;
            ViewData["AdminID"] = ID;
            return View(MyViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourseIntoClass(ClassesViewModel classroom)
        {
            var ID = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                try
                {
                    var Myclass = dbContext.Classrooms.Find(classroom.classID);

                    string uniuefilename = classroom.Image;
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

                    Myclass.Name = classroom.Name;
                    Myclass.startDate = classroom.startDate;
                    Myclass.endDate = classroom.endDate;
                    Myclass.AdminID = ID;
                    Myclass.Image = uniuefilename;

                    foreach (var item in dbContext.CourseClassrooms)
                    {
                        if (item.classID == classroom.classID)
                        {
                            dbContext.Remove(item);
                        }
                    }
                    foreach (var item in classroom.Courses)
                    {
                        if (item.IsSelected)
                        {
                            dbContext.CourseClassrooms.Add(new CourseClassroom()
                            {
                                courseID = item.courseID,
                                classID = classroom.classID
                            });
                        }
                    }
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassroomExists(classroom.classID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id=classroom.classID});
            }
            ViewData["AdminID"] = ID;
            return View(classroom);
        }

        //GET: Admin/Classroom/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var classroom = await dbContext.Classrooms.FirstOrDefaultAsync(m => m.classID == id);
            if (classroom == null)
            {
                return NotFound();
            }
            var MyViewModel = new ClassesViewModel();
            MyViewModel.classID = id.Value;
            MyViewModel.Name = classroom.Name;
            MyViewModel.startDate = classroom.startDate;
            MyViewModel.endDate = classroom.endDate;
            MyViewModel.Image = classroom.Image;
            MyViewModel.CourseList = await (from c in dbContext.Courses join co in dbContext.CourseClassrooms
                                            on c.courseID equals co.courseID
                                      where co.classID == id
                                      select new Course {
                                          TenKhoaHoc = c.TenKhoaHoc,
                                          courseID = c.courseID
                                      }).ToListAsync();
            return View(MyViewModel);
               
        }

        // POST: Admin/Classrooms/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var classroom = await dbContext.Classrooms.FindAsync(id);
            var classList = await dbContext.CourseClassrooms.Where(m => m.classID == id).ToListAsync();
            dbContext.Classrooms.Remove(classroom);
           foreach(var item in classList)
            {
                dbContext.CourseClassrooms.Remove(item);
            }
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ClassroomExists(int id)
        {
            return dbContext.Classrooms.Any(e => e.classID == id);
        }

        //GET: Admin/Classroom/CourseDetail/id
        [HttpGet]
        public IActionResult CourseDetail(int id, int classID)
        {
            ViewBag.classID = classID;
            var course = data.GetCourseDetail(id).FirstOrDefault();
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

    }
}
