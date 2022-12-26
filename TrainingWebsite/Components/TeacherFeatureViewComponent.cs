using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Components
{
    public class TeacherFeatureViewComponent : ViewComponent
    {
        private readonly ICourse data;
        public TeacherFeatureViewComponent(ICourse _data)
        {
            data = _data;
        }
        public IViewComponentResult Invoke(int courseID)
        {
            var result = data.TeacherFeatureDDetail(courseID).FirstOrDefault();
            return View(result);
        }
    }
}
