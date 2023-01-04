using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Components
{
    public class profileDetailViewComponent:ViewComponent
    {
        private readonly ICourse data;
        public profileDetailViewComponent(ICourse _data)
        {
            data = _data;
        }
        public IViewComponentResult Invoke(string Id)
        {
            var profile = data.getProfileDetail(Id).FirstOrDefault();
            return View(profile);
        }
    }
}
