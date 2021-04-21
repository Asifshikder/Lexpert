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
    public class SiteSettingController : Controller
    {
        private AppDbContext db;
        private IFileHandler fileHandler;
        public SiteSettingController(AppDbContext db, IFileHandler fileHandler)
        {
            this.db = db;
            this.fileHandler = fileHandler;
        }
        public IActionResult Index()
        {
            var list = db.SiteSetting.FirstOrDefault();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SiteSetting SiteSetting)
        {
            db.SiteSetting.Add(SiteSetting);
            db.SaveChanges();
            return Redirect("/admin/SiteSetting/Index");
        }
        public IActionResult Edit()
        {
            var model = db.SiteSetting.FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(SiteSetting SiteSetting)
        {
            db.Entry(SiteSetting).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/admin/SiteSetting/Index");
        }
    }
}
