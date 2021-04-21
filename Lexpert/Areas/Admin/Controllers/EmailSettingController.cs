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
    public class EmailSettingController : Controller
    {
        private AppDbContext db;
        private IFileHandler fileHandler;
        public EmailSettingController(AppDbContext db, IFileHandler fileHandler)
        {
            this.db = db;
            this.fileHandler = fileHandler;
        }
        public IActionResult Index()
        {
            var list = db.EmailSetting.FirstOrDefault();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmailSetting model)
        {
            db.EmailSetting.Add(model);
            db.SaveChanges();
            return Redirect("/admin/EmailSetting/Index");
        }
        public IActionResult Edit()
        {
            var model = db.EmailSetting.FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(EmailSetting model)
        {
            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/admin/EmailSetting/Index");
        }
    }
}
