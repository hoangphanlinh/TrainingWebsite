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
        public IActionResult GetCourseByJobPos(int searchString)
        {
            
            var course = (from c in data.Courses
                          join o in data.Occuptions on c.IDJobPos equals o.OccuptionID
                          join a in data.Apartments on o.ApartmentID equals a.ApartmentID
                          join u in data.Users on c.IDTrainer equals u.Id
                          where a.ApartmentID.Equals(searchString)
                          select new CourseHomeViewModel
                          {
                              Image = c.ImageTrainer,
                              TrainerName = u.Email,
                              TenKhoaHoc = c.TenKhoaHoc

                          }).ToList();
            ViewBag.search = searchString;
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
    }
}
