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
    public class ProductController : Controller
    {
        private AppDbContext db;
        private IFileHandler fileHandler;

        public ProductController(AppDbContext db,IFileHandler fileHandler)
        {
            this.db = db;
            this.fileHandler = fileHandler;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var productList = db.Product.ToList();
            return View(productList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM product)
        {
            if (product.FeatureImage1File != null)
            {
                product.FeatureImage1 = fileHandler.UploadFile("Product", product.FeatureImage1File);
            }
            if (product.FeatureImage2File != null)
            {
                product.FeatureImage2 = fileHandler.UploadFile("Product", product.FeatureImage2File);
            }
            if (product.FeatureImage3File != null)
            {
                product.FeatureImage3 = fileHandler.UploadFile("Product", product.FeatureImage3File);
            }
            if (product.FeatureImage4File != null)
            {
                product.FeatureImage4 = fileHandler.UploadFile("Product", product.FeatureImage4File);
            }
            if (product.FeatureImage5File != null)
            {
                product.FeatureImage5 = fileHandler.UploadFile("Product", product.FeatureImage5File);
            }
            if (product.ProfileImageFile != null)
            {
                product.ProfileImage = fileHandler.UploadFile("Product", product.ProfileImageFile);
            }
            if (product.ProfileImage1File != null)
            {
                product.ProfileImage1 = fileHandler.UploadFile("Product", product.ProfileImage1File);
            }
            if (product.ProfileImage2File != null)
            {
                product.ProfileImage2 = fileHandler.UploadFile("Product", product.ProfileImage2File);
            }
            Product model = new Product();
            model.ProductName = product.ProductName;
            model.Description = product.Description;
            model.CartDescription = product.CartDescription;
            model.Amazonlink = product.Amazonlink;
            model.ProfileImage = product.ProfileImage;
            model.ProfileImage1 = product.ProfileImage1;
            model.ProfileImage2 = product.ProfileImage2; 
            model.OtherDescription = product.OtherDescription;

            model.VideoName = product.VideoName;
            model.VideoUrl = product.VideoUrl;
            model.ShortDescription = product.ShortDescription;
            model.IsFeature = product.IsFeature;
            model.FeatureImage1 = product.FeatureImage1;
            model.FeatureImage2 = product.FeatureImage2;
            model.FeatureImage3 = product.FeatureImage3;
            model.FeatureImage4 = product.FeatureImage4;
            model.FeatureImage5 = product.FeatureImage5;
            db.Product.Add(model);
            db.SaveChanges();
            return Redirect("/admin/Product/Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = db.Product.Where(s => s.ProductId == id).FirstOrDefault();
            ProductVM model = new ProductVM();
            model.ProductId = product.ProductId; model.Amazonlink = product.Amazonlink;

            model.ProductName = product.ProductName;
            model.Description = product.Description;
            model.ProfileImage = product.ProfileImage;
            model.ProfileImage1 = product.ProfileImage1;
            model.ProfileImage2 = product.ProfileImage2;
            model.OtherDescription = product.OtherDescription;
            model.VideoName = product.VideoName;
            model.VideoUrl = product.VideoUrl;
            model.ShortDescription = product.ShortDescription;
            model.IsFeature = product.IsFeature;
            model.FeatureImage1 = product.FeatureImage1; model.CartDescription = product.CartDescription;

            model.FeatureImage2 = product.FeatureImage2;
            model.FeatureImage3 = product.FeatureImage3;
            model.FeatureImage4 = product.FeatureImage4;
            model.FeatureImage5 = product.FeatureImage5;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProductVM product)
        {
            if (product.FeatureImage1File != null)
            {
                product.FeatureImage1 = fileHandler.UpdateFile(product.FeatureImage1, "Product", product.FeatureImage1File);
            }
            if (product.FeatureImage2File != null)
            {
                product.FeatureImage2 = fileHandler.UpdateFile(product.FeatureImage2, "Product", product.FeatureImage2File);
            }
            if (product.FeatureImage3File != null)
            {
                product.FeatureImage3 = fileHandler.UpdateFile(product.FeatureImage3, "Product", product.FeatureImage3File);
            }
            if (product.FeatureImage4File != null)
            {
                product.FeatureImage4 = fileHandler.UpdateFile(product.FeatureImage4, "Product", product.FeatureImage4File);
            }
            if (product.FeatureImage5File != null)
            {
                product.FeatureImage5 = fileHandler.UpdateFile(product.FeatureImage5, "Product", product.FeatureImage5File);
            }
            if (product.ProfileImageFile != null)
            {
                product.ProfileImage = fileHandler.UpdateFile(product.ProfileImage, "Product", product.ProfileImageFile);
            }
            if (product.ProfileImage1File != null)
            {
                product.ProfileImage1 = fileHandler.UpdateFile(product.ProfileImage1, "Product", product.ProfileImage1File);
            }
            if (product.ProfileImage2File != null)
            {
                product.ProfileImage2 = fileHandler.UpdateFile(product.ProfileImage2, "Product", product.ProfileImage2File);
            }
            Product model = new Product();
            model.ProductId = product.ProductId;
            model.Amazonlink = product.Amazonlink;

            model.ProductName = product.ProductName;
            model.Description = product.Description;
            model.ProfileImage = product.ProfileImage;
            model.ProfileImage1 = product.ProfileImage1;
            model.ProfileImage2 = product.ProfileImage2;
            model.VideoName = product.VideoName;
            model.VideoUrl = product.VideoUrl;
            model.ShortDescription = product.ShortDescription;
            model.IsFeature = product.IsFeature;    
            model.CartDescription = product.CartDescription;
            model.OtherDescription = product.OtherDescription;

            model.FeatureImage1 = product.FeatureImage1;
            model.FeatureImage2 = product.FeatureImage2;
            model.FeatureImage3 = product.FeatureImage3;
            model.FeatureImage4 = product.FeatureImage4;
            model.FeatureImage5 = product.FeatureImage5;
            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/admin/Product/Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = db.Product.Where(s => s.ProductId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(Product product)
        {
            db.Product.Remove(product);
            db.SaveChanges();
            return Redirect("/admin/Product/Index");
        }
    }
}
