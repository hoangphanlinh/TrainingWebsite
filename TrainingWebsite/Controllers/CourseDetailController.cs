using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Data;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Controllers
{
    public class CourseDetailController : Controller
    {
        private readonly ApplicationDbContext data;
        public CourseDetailController (ApplicationDbContext _data)
        {
            data = _data;
        }
        public IActionResult Index(int id)
        {
            ViewBag.Id = id;
            CourseDetailViewModel topicList = new CourseDetailViewModel();
            topicList.TopicList = (from t in data.Topic
                                   where t.IDKhoaHoc == id
                                   select new TopicListViewModel()
                                   {
                                       topicID = t.IDChuDe,
                                       topicName = t.TenChuDe,
                                       Noidung = t.NoiDung
                                   }).ToList();
          
            return View();
        }
    }
}
