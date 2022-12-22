using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Manager.Models;
using TrainingWebsite.Data;
using TrainingWebsite.Models;
using TrainingWebsite.ViewModels;
using X.PagedList;

namespace TrainingWebsite.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly ICourse _course;
        public CourseController(ApplicationDbContext _data, ICourse _course)
        {
            this.data = _data;
            this._course = _course;
        }
        public int pageSize = 4;
        public IActionResult Index(int? size, int? page, string txtSearch, int JobPosID = 0, int ApartmentID = 0)
        {
            ViewBag.txtSearch = txtSearch;

            ViewBag.CategoryID = _course.OccuptionDropDown();

            ViewBag.JobPosID = JobPosID;

            ViewBag.ApartID = ApartmentID;

            var data = _course.getCourse_JobPos_ApartmentAll();

            if (!string.IsNullOrEmpty(txtSearch))
            {
                data = data.Where(x => x.TenKhoaHoc.ToUpper().Contains(txtSearch.ToUpper()));
            }

            if (JobPosID != 0)
            {
                data = data.Where(c => c.JobPosID == JobPosID);
            }

            if(ApartmentID != 0)
            {
                data = data.Where(a => a.ApartID == ApartmentID);
            }
            // 3 Đoạn code sau dùng để phân trang
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
            int pageSize = (size ?? 4);

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
