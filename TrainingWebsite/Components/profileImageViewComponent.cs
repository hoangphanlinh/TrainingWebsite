using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Components
{
    public class profileImageViewComponent : ViewComponent
    {
        private readonly ICourse data;
        public profileImageViewComponent(ICourse _data)
        {
            data = _data;
        }
        public IViewComponentResult Invoke(string Id)
        {

            var result = data.getProfileImage(Id).FirstOrDefault();
            return View(result);
        }

    }
}
