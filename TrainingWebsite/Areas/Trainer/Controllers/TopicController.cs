using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Trainer.Models;
using TrainingWebsite.Data;
using TrainingWebsite.Models;

namespace TrainingWebsite.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    public class TopicController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IWebHostEnvironment WebHostEnvironment;
        public TopicController(ApplicationDbContext _data, IWebHostEnvironment WebHostEnvironment)
        {
            this.data = _data;
            this.WebHostEnvironment = WebHostEnvironment;
        }
       [HttpGet]
        public IActionResult Index(int id)
        {
            ViewBag.sessionID = id;

            ViewBag.courseName = (from course in data.Courses where course.ID == id select course.TenKhoaHoc).SingleOrDefault();

            var session = (from s in data.Topic
                           where s.IDKhoaHoc == id
                           select new SessionListViewModel
                           {
                               ID = s.IDChuDe,
                               TenChuDe = s.TenChuDe
                           }).ToList();

            return View(session);
        }
       
        public IActionResult CreateTopic(int id)
        {
            ViewBag.ID = id;

            return View();
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<IActionResult> CreateTopic(CreateTopicViewModel model,int id)
        {
            try
            {
                var session = data.Topic.Where(m => m.IDKhoaHoc == id);

                if (ModelState.IsValid)
                {
                    string uniuefilename = string.Empty;

                    var files = HttpContext.Request.Form.Files;

                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;

                            var uploads = Path.Combine(WebHostEnvironment.WebRootPath, "Video");
                            if (file.Length > 0)
                            {
                                // var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                                var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    uniuefilename = fileName;
                                }

                            }
                        }
                    }
                    var chude = new ChuDe()
                    {
                        NoiDung = uniuefilename,
                        IDKhoaHoc = id,
                        TenChuDe = model.TenChuDe,
        
                       
                    };
                    data.Topic.Add(chude);
                    await data.SaveChangesAsync();
                    return RedirectToAction("Index", "Topic", new { id = id });
                }
              
            }
            catch(Exception ex)
            {
                ViewBag.mess = $"{ex.Message}";
            }
            return View(model);
        }
    }
}
