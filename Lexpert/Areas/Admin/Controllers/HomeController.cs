using Lexpert.Models;
using Lexpert.Models.ViewModel;
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
    public class HomeController : Controller
    {
        private AppDbContext db;
        public HomeController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            DashboadViewModel model = new DashboadViewModel();
            model.SubscribtionCount = db.Warranty.Count();
            model.TotalProduct = db.Product.Count();
            model.FeaturedProduct = db.Product.Where(s=>s.IsFeature==true).Count();
            var siteset = db.SiteSetting.FirstOrDefault();
            if (siteset != null)
            {
  model.BlogInfomation = siteset.IsBlogEnabled == true ? "Blog is enable" : "Blog is hidden from the client site";
            model.FooterInformation = siteset.UseSmallFooter == true ? "Site is using small footer" : "Site is using big footer";
          
            }
            return View(model);
        }
    }
}
