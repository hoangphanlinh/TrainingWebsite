using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Manager.Models;
using TrainingWebsite.Data;
using TrainingWebsite.Models;
using TrainingWebsite.ViewModels;


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
        public IActionResult Index(int? page = 0)
        {
            int limit = 4;
            int start;
            if(page > 0)
            {
#pragma warning disable CS1717 // Assignment made to same variable
                page = page;
#pragma warning restore CS1717 // Assignment made to same variable
            }
            else
            {
                page = 1;
            }
            start = (int)(page - 1) * limit;
            ViewBag.pageCurrent = page;
            int totalCourse = _course.totalCourse();

            ViewBag.totalCourse = totalCourse;

            ViewBag.numberPage = _course.numberPage(totalCourse, limit);
            var model = _course.paginationCourse(start, limit);

            return View(model);
        }
    }
}
