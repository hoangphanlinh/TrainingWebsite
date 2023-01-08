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
    public class TaiLieusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment WebHostEnvironment;


        public TaiLieusController(ApplicationDbContext context, IWebHostEnvironment _WebHostEnvironment)
        {
            _context = context;
            WebHostEnvironment = _WebHostEnvironment;
        }

        // GET: Trainer/TaiLieus
        public async Task<IActionResult> Index(int chudeID)
        {
            ViewData["chudeID"] = chudeID;
            var model = await _context.TaiLieus.Where(x => x.ChuDeID == chudeID).ToListAsync();
            return View(model);
        }

        // GET: Trainer/TaiLieus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var taiLieu = await (from tl in _context.TaiLieus
                                 where tl.ID == id
                                 select new taiLieuViewModel {
                                     TailieuID = tl.ID,
                                     Name = tl.Name,
                                     Path = tl.Path,
                                    chudeID = tl.ChuDeID
                                 }).FirstOrDefaultAsync();
            if (taiLieu == null)
            {
                return NotFound();
            }

            return View(taiLieu);
        }
        public FileResult DowloadFile(string fileName)
        {
            string path = Path.Combine(this.WebHostEnvironment.WebRootPath, "taiLieus/") + fileName;
            using FileStream fs = System.IO.File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", fileName);
        }
        // GET: Trainer/TaiLieus/Create
        public IActionResult Create(int chudeID)
        {
            ViewData["ChuDeID"] = chudeID;

            return View();
        }

        // POST: Trainer/TaiLieus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int chudeID, createTaiLieuViewModel model)
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
                TaiLieu newTaiLieu = new TaiLieu
                {
                    Name = model.Name,
                    Path = uniqueFileName,
                    ChuDeID = chudeID
                };
                _context.TaiLieus.Add(newTaiLieu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index",new { chudeID = model.chudeID});
            }
            return View();
        }

        // GET: Trainer/TaiLieus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiLieu = await _context.TaiLieus.FindAsync(id);
            if (taiLieu == null)
            {
                return NotFound();
            }
            var myViewModel = new createTaiLieuViewModel();
            myViewModel.TailieuID = id.Value;
            myViewModel.Name = taiLieu.Name;
            myViewModel.Path = taiLieu.Path;
            myViewModel.chudeID = taiLieu.ChuDeID;
            ViewData["TaiLieuID"] = id.Value;
            ViewData["ChuDeID"] = taiLieu.ChuDeID;
            ViewBag.path = taiLieu.Path;
            return View(myViewModel);
        }

        // POST: Trainer/TaiLieus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(createTaiLieuViewModel taiLieu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TaiLieu model = _context.TaiLieus.Where(p => p.ID == taiLieu.TailieuID).FirstOrDefault();
                    if (model == null)
                    {
                        return NotFound();
                    }

                    string uniqueFileName = model.Path;

                    // If the Photos property on the incoming model object is not null and if count > 0,
                    // then the user has selected at least one file to upload

                    if (taiLieu.files != null && taiLieu.files.Count > 0)
                    {
                        // Loop thru each selected file
                        foreach (IFormFile photo in taiLieu.files)
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
                    model.Name = taiLieu.Name;
                    model.Path = uniqueFileName;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiLieuExists(taiLieu.TailieuID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index",new {chudeID = taiLieu.chudeID});
            }
            ViewData["ChuDeID"] = taiLieu.chudeID;
            return View(taiLieu);
        }

        // GET: Trainer/TaiLieus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiLieu = await _context.TaiLieus
                .Include(t => t.chuDe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (taiLieu == null)
            {
                return NotFound();
            }

            return View(taiLieu);
        }

        // POST: Trainer/TaiLieus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taiLieu = await _context.TaiLieus.FindAsync(id);
            _context.TaiLieus.Remove(taiLieu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiLieuExists(int id)
        {
            return _context.TaiLieus.Any(e => e.ID == id);
        }
    }
}
