using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TrainingWebsite.Areas.Trainer.Models;
using TrainingWebsite.Data;
using TrainingWebsite.Models;

namespace TrainingWebsite.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    [Authorize(Roles = "Trainer")]

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

            ViewBag.courseName = (from course in data.Courses where course.courseID == id select course.TenKhoaHoc).SingleOrDefault();

            var session = (from s in data.Topic
                           where s.IDKhoaHoc == id
                           select new SessionListViewModel
                           {
                               ID = s.IDChuDe,
                               TenChuDe = s.TenChuDe,
                               NoiDung = s.NoiDung

                           }).ToList();

            return View(session);
        }

        public IActionResult CreateTopic(int id)
        {
            ViewBag.ID = id;

            return View();
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)] //Gioi han file tai len 50MB
        public async Task<IActionResult> CreateTopic(CreateTopicViewModel model, int id)
        {
            try
            {
                var session = data.Topic.Where(m => m.IDKhoaHoc == id);

                if (ModelState.IsValid)
                {
                    string uniuefilename = string.Empty;

                    //Tai file tu server
                    var files = HttpContext.Request.Form.Files;

                    foreach (var video in files)
                    {
                        if (video != null && video.Length > 0)
                        {
                            var file = video;

                            var uploads = Path.Combine(WebHostEnvironment.WebRootPath, "Video");
                            if (file.Length > 0)
                            {
                                // var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName); // tao ten file
                                var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    uniuefilename = fileName;
                                }

                            }
                        }
                    }

                    ////Xu ly copy link youtube tu thanh dia chi

                    //var videoUrl = new Uri(model.NoiDung);
                    //    var videoID = HttpUtility.ParseQueryString(videoUrl.Query).Get("v");


                    //    var chude = new ChuDe()
                    //    {

                    //        NoiDung = videoID,
                    //        IDKhoaHoc = id,
                    //        TenChuDe = model.TenChuDe,
                    //    };

                    //    data.Topic.Add(chude);
                    //    await data.SaveChangesAsync();

                    //    return RedirectToAction("Index", "Topic", new { id = id });



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
            catch (Exception ex)
            {
                ViewBag.mess = $"{ex.Message}";
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteTopic(int id)
        {

            var topic = await data.Topic.SingleOrDefaultAsync(m => m.IDChuDe == id);
            data.Topic.Remove(topic);
            await data.SaveChangesAsync();
            return RedirectToAction("Index", "Topic", new { id = topic.IDKhoaHoc });

        }
        public async Task<IActionResult> EditTopic(int id)
        {
            var topic = await data.Topic.SingleOrDefaultAsync(m => m.IDChuDe == id);
            ViewBag.ID = topic.IDKhoaHoc;

            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);

        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)] //Gioi han file tai len 50MB
        public async Task<IActionResult> EditTopic(int id, ChuDe chuDe)
        {
            var topic = await data.Topic.SingleOrDefaultAsync(m => m.IDChuDe == id);

            if (ModelState.IsValid)
            {
                string uniuefilename = string.Empty;

                //Tai file tu server
                var files = HttpContext.Request.Form.Files;

                foreach (var video in files)
                {
                    if (video != null && video.Length > 0)
                    {
                        var file = video;

                        var uploads = Path.Combine(WebHostEnvironment.WebRootPath, "Video");
                        if (file.Length > 0)
                        {
                            // var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName); // tao ten file
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                uniuefilename = fileName;
                            }

                        }
                    }
                }
                topic.TenChuDe = chuDe.TenChuDe;
                topic.NoiDung = uniuefilename;
                await data.SaveChangesAsync();

                return RedirectToAction("Index", "Topic", new { id = topic.IDKhoaHoc });

            }
            return View();

        }
    }
}
