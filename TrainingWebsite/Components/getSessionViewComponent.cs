using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Components
{
    public class getSessionViewComponent : ViewComponent
    {
        private readonly ICourse data;
        public getSessionViewComponent(ICourse _data)
        {
            data = _data;
        }
        public IViewComponentResult Invoke(int courseId)
        {
            var result = data.GetListTopic(courseId);
            return View(result);
        }
    }
}
