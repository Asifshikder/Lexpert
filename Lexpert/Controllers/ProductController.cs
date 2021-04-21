using Lexpert.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext db;
        public ProductController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var mdoels = db.Product.ToList();
            return View(mdoels);
        }
        public IActionResult Details(int id)
        {
            var mdoels = db.Product.Where(s=>s.ProductId==id).FirstOrDefault();
            return View(mdoels);
        }
    }
}
