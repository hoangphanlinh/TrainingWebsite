using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Components
{
    public class Detail_InfoViewComponent : ViewComponent
    {
        private readonly ICourse data;
        public Detail_InfoViewComponent(ICourse _data)
        {
            data = _data;
        }
        public IViewComponentResult Invoke(int course_id)
        {
            var course = data.GetCourseDetail(course_id).FirstOrDefault();
            return View(course);
        }

    }
}
