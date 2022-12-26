using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Components
{
    public class FeatureCourseViewComponent :ViewComponent
    {
        private readonly ICourse data;
        public FeatureCourseViewComponent(ICourse _data)
        {
            data = _data;
        }
        public IViewComponentResult Invoke(int courseId)
        {
            var result = data.CourseFeatureDDetail(courseId).FirstOrDefault();
            return View(result);
        }
    }
}
