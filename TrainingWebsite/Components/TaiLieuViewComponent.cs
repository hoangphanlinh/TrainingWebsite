using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Data;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Components
{
    public class TaiLieuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext data;

        public TaiLieuViewComponent(ApplicationDbContext _data)
        {
            data = _data;
        }
        public IViewComponentResult Invoke(int chudeID)
        {
            ViewBag.chudeID = chudeID;
            var model = (from tl in data.TaiLieus
                         where tl.ChuDeID == chudeID
                         select new ViewDetailViewModel
                         {
                             FileName = tl.Name,
                             Path = tl.Path
                         }).ToList();
            return View(model);
        }
    }
}
