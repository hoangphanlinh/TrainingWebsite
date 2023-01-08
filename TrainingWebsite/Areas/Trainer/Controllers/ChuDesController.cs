using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingWebsite.Data;
using TrainingWebsite.Models;

namespace TrainingWebsite.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    [Authorize(Roles = "Trainer")]
    public class ChuDesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChuDesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trainer/ChuDes
        public async Task<IActionResult> Index(int id)
        {

            ViewData["id"] = id;
            var model = await _context.Topic.Where(x => x.IDKhoaHoc == id).ToListAsync();
            ModelState.Remove("id");
            return View(model);
        }

        // GET: Trainer/ChuDes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _context.Topic
                .Include(c => c.course)
                .FirstOrDefaultAsync(m => m.IDChuDe == id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        // GET: Trainer/ChuDes/Create
        public IActionResult Create(int id)
        {
            ViewData["IDKhoaHoc"] = id;
            return View();
        }

        // POST: Trainer/ChuDes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDChuDe,TenChuDe,NoiDung,IDKhoaHoc")] ChuDe chuDe)
        {
            ModelState.Remove("id");
            if (ModelState.IsValid)
            {
                _context.Add(chuDe);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id= chuDe.IDKhoaHoc});
            }
                       
            return View(chuDe);
        }

        // GET: Trainer/ChuDes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id,int courseID)
        {
            ModelState.Remove("id");

            ViewBag.IDKhoaHoc = courseID;
            ViewBag.IDTopic = id;
            var topic = await _context.Topic.Where(p => p.IDChuDe == id && p.IDKhoaHoc == courseID).FirstOrDefaultAsync();
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Trainer/ChuDes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,int courseID, ChuDe chuDe)
        {
            ModelState.Remove("id");

            if (ModelState.IsValid)
            {
                    ChuDe topic = _context.Topic.Where(p => p.IDChuDe == chuDe.IDChuDe).FirstOrDefault();
                    if(topic == null)
                {
                    return NotFound();
                }
                    topic.TenChuDe = chuDe.TenChuDe;
                    topic.NoiDung = chuDe.NoiDung;
                    await _context.SaveChangesAsync();
               
                return RedirectToAction("Index",new { id = chuDe.IDKhoaHoc});
            }
            ViewData["IDKhoaHoc"] = courseID;
            ViewBag.IDTopic = id;
            return View(chuDe);
        }

        // GET: Trainer/ChuDes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _context.Topic
                .Include(c => c.course)
                .FirstOrDefaultAsync(m => m.IDChuDe == id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        // POST: Trainer/ChuDes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chuDe = await _context.Topic.FindAsync(id);
            _context.Topic.Remove(chuDe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChuDeExists(int id)
        {
            return _context.Topic.Any(e => e.IDChuDe == id);
        }
    }
}
