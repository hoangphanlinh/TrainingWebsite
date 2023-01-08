using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingWebsite.Data;
using TrainingWebsite.Models;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext db;
        public ChatController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var comments = db.Comments.Include(x => x.Replies).ThenInclude(x => x.User).OrderByDescending(x => x.CreateOn)
                .ToList();
            return View(comments);
        }

        //Post:ChatRoom/PostReply
        [HttpPost]
        public ActionResult PostReply(ReplyVM obj)
        {

            ISession session = HttpContext.Session;
            string userId = session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Reply r = new Reply();
            r.Text = obj.Reply;
            r.CommentId = obj.CID;
            r.UserId = userId;
            r.CreateOn = DateTime.Now;
            db.Replies.Add(r);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Post:ChatRoom/PostComment
        [HttpPost]
        public ActionResult PostComment(string CommentText)
        {
            ISession session = HttpContext.Session;
            string userId = session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Comment c = new Comment();
            c.Text = CommentText;
            c.CreateOn = DateTime.Now;
            c.UserId = userId;
            db.Comments.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
