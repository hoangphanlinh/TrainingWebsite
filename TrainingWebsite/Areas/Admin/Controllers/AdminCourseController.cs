using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Models;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCourseController : Controller
    {
        private readonly ICourse _course;
        public AdminCourseController(ICourse course)
        {
            _course = course;
        }
        public IActionResult Index()
        {
            var courses = _course.getCourseAll();
            return View(courses);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("MaKhoaHoc,TenKhoaHoc,ThoiLuongKhoaHoc,MucTieuKhoaHoc,HinhThucDanhGia,IDKhoaHocTienQuyet,IDTrainer,ImageTrainer,IDJobPos")] Course course)
        {
            if (ModelState.IsValid)
            {
                _course.Add(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = _course.FindById(Convert.ToInt32(id));
            if(course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = _course.FindById(Convert.ToInt32(id));
            if(course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("MaKhoaHoc,TenKhoaHoc,ThoiLuongKhoaHoc,MucTieuKhoaHoc,HinhThucDanhGia,IDKhoaHocTienQuyet,IDTrainer,ImageTrainer,IDJobPos")] Course course)
        {
            if (ModelState.IsValid)
            {
                _course.Edit(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _course.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
