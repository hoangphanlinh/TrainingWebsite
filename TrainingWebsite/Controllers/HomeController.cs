using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TrainingWebsite.Areas.Identity.Data;
using TrainingWebsite.Data;
using TrainingWebsite.Models;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration Configuration;
        private readonly ICourse _course;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ICourse course)
        {
            _logger = logger;
            this.Configuration = configuration;
            this._course = course;
        }
      
        public IActionResult Index()
        {
            var courses = _course.getCourseAll().Take(6);

            return View(courses);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                //Read SMTP settings from AppSettings.json.
                string host = this.Configuration.GetValue<string>("Smtp:Server");
                int port = this.Configuration.GetValue<int>("Smtp:Port");
                string ToAddress = this.Configuration.GetValue<string>("Smtp:ToAddress");
                string userName = this.Configuration.GetValue<string>("Smtp:UserName");
                string password = this.Configuration.GetValue<string>("Smtp:Password");

                using (MailMessage mm = new MailMessage(model.Email, ToAddress))
                {
                    mm.Subject = model.Subject;
                    mm.Body = "Name: " + model.Name + "<br /><br />Email: " + model.Email + "<br />" + model.Message;
                    mm.IsBodyHtml = true;

                    //Dinh kem file
                    //if (model.Attachment.Length > 0)
                    //{
                    //    string fileName = Path.GetFileName(model.Attachment.FileName);
                    //    mm.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
                    //}

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = host;
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(userName, password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = port;
                        smtp.Send(mm);
                        ViewBag.Message = "Email sent sucessfully! Responding in a few minutes...";
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Course()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
