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
    public class BlogController : Controller
    {
        private AppDbContext db;
        private IFileHandler fileHandler;

        public BlogController(AppDbContext db, IFileHandler fileHandler)
        {
            this.db = db;
            this.fileHandler = fileHandler;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var BlogList = db.BlogPost.ToList();
            return View(BlogList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogPostVM Blog)
        {
            if (Blog.BlogImageFile != null)
            {
                Blog.BlogImage = fileHandler.UploadFile("Blog", Blog.BlogImageFile);
            }
            
            BlogPost model = new BlogPost();
            model.BlogTitle = Blog.BlogTitle;
            model.BlogImage = Blog.BlogImage;
            model.Content = Blog.Content;
            model.PostTime = DateTime.Now;
            db.BlogPost.Add(model);
            db.SaveChanges();
            return Redirect("/admin/Blog/Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var Blog = db.BlogPost.Where(s => s.BlogId == id).FirstOrDefault();
            return View(Blog);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Blog = db.BlogPost.Where(s => s.BlogId == id).FirstOrDefault();
            BlogPostVM model = new BlogPostVM();
            model.BlogId = Blog.BlogId;
            model.BlogTitle = Blog.BlogTitle;
            model.PostTime = DateTime.Now;
            model.BlogImage = Blog.BlogImage;
            model.Content = Blog.Content;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(BlogPostVM Blog)
        {
            if (Blog.BlogImageFile != null)
            {
                Blog.BlogImage = fileHandler.UpdateFile(Blog.BlogImage,"Blog", Blog.BlogImageFile);
            }

            BlogPost model = new BlogPost();
            model.BlogId = Blog.BlogId;
            model.BlogTitle = Blog.BlogTitle;
            model.PostTime = DateTime.Now;
            model.BlogImage = Blog.BlogImage;
            model.Content = Blog.Content;
            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/admin/Blog/Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Blog = db.BlogPost.Where(s => s.BlogId == id).FirstOrDefault();
            return View(Blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(BlogPost Blog)
        {
            db.BlogPost.Remove(Blog);
            db.SaveChanges();
            return Redirect("/admin/Blog/Index");
        }
    }
}
