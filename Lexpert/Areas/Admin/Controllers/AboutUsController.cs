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
    public class AboutUsController : Controller
    {
        private AppDbContext db;
        private IFileHandler fileHandler;
        public AboutUsController(AppDbContext db, IFileHandler fileHandler)
        {
            this.db = db;
            this.fileHandler = fileHandler;
        }
        public IActionResult Index()
        {
            var list = db.AboutUs.FirstOrDefault();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AboutUsModel aboutUs)
        {
            if (aboutUs.ImageFile != null)
            {
                aboutUs.ImageUrl = fileHandler.UploadFile("AboutUs", aboutUs.ImageFile);
            }
            AboutUs model = new AboutUs();
            model.Title = aboutUs.Title;
            model.Details = aboutUs.Details;
            model.ImageUrl = aboutUs.ImageUrl;
            db.AboutUs.Add(model);
            db.SaveChanges();
            return Redirect("/admin/Aboutus/Index");
        }
        public IActionResult Edit()
        {
            var model = db.AboutUs.FirstOrDefault();
            AboutUsModel aboutUs = new AboutUsModel();
            aboutUs.AboutUsId = model.AboutUsId;
            aboutUs.Title = model.Title;
            aboutUs.Details = model.Details;
            aboutUs.ImageUrl = model.ImageUrl;
            return View(aboutUs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AboutUsModel aboutUs)
        {
            if (aboutUs.ImageFile != null)
            {
                aboutUs.ImageUrl = fileHandler.UpdateFile(aboutUs.ImageUrl,"AboutUs", aboutUs.ImageFile);
            }
            AboutUs model = new AboutUs();
            model.AboutUsId = aboutUs.AboutUsId;
            model.Title = aboutUs.Title;
            model.Details = aboutUs.Details;
            model.ImageUrl = aboutUs.ImageUrl;
            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/admin/Aboutus/Index");
        }
    }
}
