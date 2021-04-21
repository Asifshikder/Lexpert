using Lexpert.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NavlinkController : Controller
    {
        private AppDbContext db;
        public NavlinkController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var list = db.NavlinkSetup.FirstOrDefault();
            NavlinkVM navlink = new NavlinkVM();
           
            if (list != null)
            {
                navlink.Id = list.SetupId;
                navlink.BasicName = db.Product.Where(s => s.ProductId == list.BasicProductId).FirstOrDefault().ProductName;
                navlink.AdvanceName = db.Product.Where(s => s.ProductId == list.AdvanceProductId).FirstOrDefault().ProductName;
                navlink.UltimateName = db.Product.Where(s => s.ProductId == list.UltimateProductId).FirstOrDefault().ProductName;
            }
            else
            {
 navlink = null;
            }
            return View(navlink);
        }
        public IActionResult Create()
        {

            ViewBag.products = new SelectList(db.Product.Select(s => new { Id = s.ProductId, Name = s.ProductName }), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NavlinkSetup model)
        {
            db.NavlinkSetup.Add(model);
            db.SaveChanges();
            return Redirect("/admin/Navlink/Index");
        }
        public IActionResult Edit()
        {
            ViewBag.products = new SelectList(db.Product.Select(s => new { Id = s.ProductId, Name = s.ProductName }), "Id", "Name");

            var model = db.NavlinkSetup.FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(NavlinkSetup model)
        {

            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/admin/Navlink/Index");
        }
    }
    public class NavlinkVM
    {
        public int Id { get; set; }
        public string BasicName { get; set; }
        public string AdvanceName { get; set; }
        public string UltimateName { get; set; }


    }

}
