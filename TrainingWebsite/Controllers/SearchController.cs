using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Data;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext data;
        public SearchController(ApplicationDbContext data)
        {
            
            this.data = data;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult GetCourseByJobPos(int id)
        {
            var aprartName = data.Apartments.Where(x => x.ApartmentID == id).FirstOrDefault().ApartmentName;

            var course = (from c in data.Courses
                          join o in data.Occuptions on c.IDJobPos equals o.OccuptionID
                          join u in data.Users on c.IDTrainer equals u.Id
                          where o.ApartmentID == id
                          select new CourseHomeViewModel
                          {
                              Image = c.ImageTrainer,
                              TrainerName = u.Email,
                              TenKhoaHoc = c.TenKhoaHoc

                          }).ToList(); ;
            ViewBag.Name = aprartName;
            if (course == null)
            {
                return NotFound();
            }
            else
            {
                return View(course);
            }
        }
    }
}
