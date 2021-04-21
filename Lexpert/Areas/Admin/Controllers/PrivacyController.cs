using Lexpert.Areas.Admin.Models;
using Lexpert.Helpers;
using Lexpert.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PrivacyController : Controller
    {
        private AppDbContext db;
        private IFileHandler fileHandler;
        public PrivacyController(AppDbContext db, IFileHandler fileHandler)
        {
            this.db = db;
            this.fileHandler = fileHandler;
        }
        public IActionResult Index()
        {
            var list = db.Privacy.FirstOrDefault();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Privacy model)
        {
            db.Privacy.Add(model);
            db.SaveChanges();
            return Redirect("/admin/Privacy/Index");
        }
        public IActionResult Edit()
        {
            var model = db.Privacy.FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Privacy model)
        {
            
            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/admin/Privacy/Index");
        }
    }
}
