using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;
using TrainingWebsite.Data;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment WebHostEnvironment;
        public DashboardController(ApplicationDbContext _data, UserManager<ApplicationUser> userManager, IWebHostEnvironment _WebHostEnvironment)
        {
            data = _data;
            _userManager = userManager;
            WebHostEnvironment = _WebHostEnvironment;
        }
        public IActionResult Index()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            ViewBag.userID = userID;
            return View();
        }
        public IActionResult CourseEnrollList()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            ViewBag.userID = userID;
            var model = (from tc in data.CourseTrainees 
                        join c1 in data.Courses on tc.CourseID equals c1.courseID
                        where tc.TraineeID == userID
                        select new DashboardViewModel
                        {
                            courseID = tc.CourseID,
                            TenKhoaHoc = c1.TenKhoaHoc,
                            TraineeID = userID,
                            TraineeLevel = tc.TraineeLevel,
                            ErollDate = tc.EnrollDate,
                            Status = tc.Status
                        }).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? courseID)
        {
            
            var userID = _userManager.GetUserId(HttpContext.User);
            ViewBag.userID = userID;
           
            var model = (from u in data.Users
                         join uc in data.CourseTrainees on u.Id equals uc.TraineeID
                         join c in data.Courses on uc.CourseID equals c.courseID
                         where c.courseID == courseID && u.Id == userID
                         select new DashboardViewModel()
                         {
                             courseID = uc.CourseID,
                             TenKhoaHoc = c.TenKhoaHoc,
                             ImageTrainer = u.Image,
                             ThoiLuongKH = c.ThoiLuongKhoaHoc,
                             MucTieuKH = c.MucTieuKhoaHoc,
                             HinhThucDanhGia = c.HinhThucDanhGia,
                             TraineeName = u.Email,
                             Title = uc.Title,
                             TraineeLevel = uc.TraineeLevel,
                             ErollDate = uc.EnrollDate,
                             Status = uc.Status
                         }).FirstOrDefault();
            var result = data.Topic.Where(x => x.IDKhoaHoc == courseID);
            var topicList = new List<TopicListViewModel>();
            foreach (var item in result)
            {
                topicList.Add(new TopicListViewModel
                {
                    topicID = item.IDChuDe,
                    topicName = item.TenChuDe,
                    Noidung = item.NoiDung
                });
            }
            ViewBag.courseID = model.courseID;
            model.topicList = topicList;
            return View(model);
        }
        public IActionResult View(int chudeID)
        {
            ViewBag.chudeID = chudeID;
            
            return View();
        }
        public FileResult DowloadFile(string fileName)
        {
            string path = Path.Combine(this.WebHostEnvironment.WebRootPath, "taiLieus/") + fileName;
            using FileStream fs = System.IO.File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
