using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingWebsite.Data;
using TrainingWebsite.Models;
using TrainingWebsite.ViewModels;

namespace TrainingWebsite.Areas.Trainer.Controllers
{
    [Area("Trainer")]
    [Authorize(Roles = "Trainer")]

    public class BaiTapsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment WebHostEnvironment;


        public BaiTapsController(ApplicationDbContext context, IWebHostEnvironment _WebHostEnvironment)
        {
            _context = context;
            WebHostEnvironment = _WebHostEnvironment;
        }

        // GET: Trainer/BaiTaps
        public async Task<IActionResult> Index(int id)
        {
            ViewData["courseID"] = id;
            var model = await _context.BaiTaps.Where(x => x.courseID == id).ToListAsync();
            return View(model);
        }

        // GET: Trainer/BaiTaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiTap = await _context.BaiTaps
                .Include(b => b.course)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (baiTap == null)
            {
                return NotFound();
            }

            return View(baiTap);
        }

        // GET: Trainer/BaiTaps/Create
        public IActionResult Create(int id)
        {
            ViewData["courseID"] = id;
            return View();
        }

        // POST: Trainer/BaiTaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,createBaiTapViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // If the Photos property on the incoming model object is not null and if count > 0,
                // then the user has selected at least one file to upload

                if (model.files != null && model.files.Count > 0)
                {
                    // Loop thru each selected file
                    foreach (IFormFile photo in model.files)
                    {
                        // The file must be uploaded to the images folder in wwwroot
                        // To get the path of the wwwroot folder we are using the injected
                        // IHostingEnvironment service provided by ASP.NET Core
                        string uploadsFolder = Path.Combine(WebHostEnvironment.WebRootPath, "taiLieus");
                        // To make sure the file name is unique we are appending a new
                        // GUID value and and an underscore to the file name
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        // Use CopyTo() method provided by IFormFile interface to
                        // copy the file to wwwroot/images folder
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                BaiTap baiTap = new BaiTap
                {
                    
                    NoiDung = uniqueFileName,
                    CreateDate = model.startDate,
                    courseID = model.courseID,
                };
                _context.BaiTaps.Add(baiTap);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = model.courseID});
            }
           
            return View();
        }

        // GET: Trainer/BaiTaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiTap = await _context.BaiTaps.FindAsync(id);
            if (baiTap == null)
            {
                return NotFound();
            }
            ViewData["courseID"] = new SelectList(_context.Courses, "courseID", "courseID", baiTap.courseID);
            return View(baiTap);
        }

        // POST: Trainer/BaiTaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NoiDung,CreateDate,courseID")] BaiTap baiTap)
        {
            if (id != baiTap.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baiTap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaiTapExists(baiTap.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["courseID"] = new SelectList(_context.Courses, "courseID", "courseID", baiTap.courseID);
            return View(baiTap);
        }

        // GET: Trainer/BaiTaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiTap = await _context.BaiTaps
                .Include(b => b.course)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (baiTap == null)
            {
                return NotFound();
            }

            return View(baiTap);
        }

        // POST: Trainer/BaiTaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baiTap = await _context.BaiTaps.FindAsync(id);
            _context.BaiTaps.Remove(baiTap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaiTapExists(int id)
        {
            return _context.BaiTaps.Any(e => e.ID == id);
        }
    }
}
