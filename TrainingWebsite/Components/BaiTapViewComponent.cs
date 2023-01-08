using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Data;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Components
{
    public class BaiTapViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext data;

        public BaiTapViewComponent(ApplicationDbContext _data)
        {
            data = _data;
        }
        public IViewComponentResult Invoke(int courseID)
        {
            ViewBag.courseID = courseID;
            var model = (from tl in data.BaiTaps
                         where tl.courseID == courseID
                         select new ViewDetailViewModel
                         {
                             NoiDung = tl.NoiDung,
                             CreateDate = tl.CreateDate
                         }).ToList();
            return View(model);
        }
    }
}
