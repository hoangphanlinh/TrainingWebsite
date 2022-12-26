using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Components
{
    public class LatestCourseViewComponent : ViewComponent
    {
        private readonly ICourse data;
        public LatestCourseViewComponent(ICourse _data)
        {
            data = _data;
        }
        public IViewComponentResult Invoke(int courseID)
        {
            var result = data.getLatestCourse(courseID).ToList().TakeLast(3);
            return View(result);
        }
    }
}
