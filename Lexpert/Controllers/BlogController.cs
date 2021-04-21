using Lexpert.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Controllers
{
    public class BlogController : Controller
    {
        private AppDbContext db;
        public BlogController(AppDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var bloglist = db.BlogPost.ToList();
            return View(bloglist);
        }
        [HttpGet]
        public IActionResult Post(int id)
        {
            var bloglist = db.BlogPost.Where(s=>s.BlogId ==id).FirstOrDefault();
            return View(bloglist);
        }
    }
}
