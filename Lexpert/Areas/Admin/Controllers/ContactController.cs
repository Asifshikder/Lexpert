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
    public class ContactController : Controller
    {
        private AppDbContext db;
        private IFileHandler fileHandler;
        public ContactController(AppDbContext db, IFileHandler fileHandler)
        {
            this.db = db;
            this.fileHandler = fileHandler;
        }
        public IActionResult Index()
        {
            var list = db.ContactUs.FirstOrDefault();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactUs contactUs)
        {
            db.ContactUs.Add(contactUs);
            db.SaveChanges();
            return Redirect("/admin/Contact/Index");
        }
        public IActionResult Edit()
        {
            var model = db.ContactUs.FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ContactUs contact)
        {
            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/admin/Contact/Index");
        }
    }
}
