using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Models;
using TrainingWebsite.ViewModels;
using X.PagedList;

namespace TrainingWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminCourseController : Controller
    {
        private readonly ICourse _course;
        public AdminCourseController(ICourse course)
        {
            _course = course;
        }
        public IActionResult Index(int? page, int? size, string txtSearch, int JobPosID=0, int ApartID =0)
        {
            ViewBag.txtSearch = txtSearch;
            ViewBag.JobPosList = _course.OccuptionDropDown();
            ViewBag.JobPosID = JobPosID;
            ViewBag.ApartList = _course.ApartmentDropDown();
            ViewBag.ApartID = ApartID;
            var data = _course.getCourseInAdmin();
            if (!string.IsNullOrEmpty(txtSearch))
            {
                data = data.Where(x => x.TenKhoaHoc.ToUpper().Contains(txtSearch.ToUpper()));
            }
            if (JobPosID != 0)
            {
                data = data.Where(c => c.JobPosID == JobPosID);
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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = _course.GetCourseDetail(id.Value).FirstOrDefault();
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        public IActionResult getEmploeeList(int? size, int? page, string txtSearch, int JobPosID = 0, int LevelID = 0, int ApartID =0)
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
                data = data.Where(x => x.FullName.Contains(txtSearch));
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
    }
}
