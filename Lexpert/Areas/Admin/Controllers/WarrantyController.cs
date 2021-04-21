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
    public class WarrantyController : Controller
    {
        private AppDbContext db;
        public WarrantyController(AppDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAll()
        {
            JsonResult result;
            try
            {
                string search = HttpContext.Request.Form["search[value]"][0];
                string draw = HttpContext.Request.Form["draw"][0];
                string order = HttpContext.Request.Form["order[0][column]"][0];
                string orderDir = HttpContext.Request.Form["order[0][dir]"][0];
                int startRec = Convert.ToInt32(HttpContext.Request.Form["start"][0]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Form["length"][0]);
                int totalRecords = 0;
                var firstPartOfQuery = db.Warranty.OrderByDescending(s=>s.AvailDate).AsQueryable();
                int ifSearch = 0;
                var secondPartOfQuery = firstPartOfQuery;
                List<WarrantyVM> data = new List<WarrantyVM>();
                
                if (secondPartOfQuery.Count() > 0)
                {
                    totalRecords = secondPartOfQuery.AsEnumerable().Count();
                    data = secondPartOfQuery.AsEnumerable().Skip(startRec).Take(pageSize).Select(

                            s => new WarrantyVM
                            {
                                WarrantyId = s.WarrantyId,
                                FullName = s.FullName,
                                Email = s.Email,
                                OrderCode = s.OrderCode,
                                AvailDate = s.AvailDate,
                                PurchaseLike = s.IsLikePurchase==true?"Yes":"No",
                            })
                        .OrderBy(s=>s.AvailDate).ToList();
                }

                data = this.SortByColumnWithOrderforEmployee(order, orderDir, data);
                int recFilter = (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search)) ? ifSearch : firstPartOfQuery.AsEnumerable().Count();

                result = this.Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = data
                });

                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Json(new
                {
                    draw = 0,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<WarrantyVM>()
                });
            }
        }

        private List<WarrantyVM> SortByColumnWithOrderforEmployee(string order, string orderDir, List<WarrantyVM> data)
        {
            List<WarrantyVM> lst = new List<WarrantyVM>();
            try
            {
                switch (order)
                {

                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.AvailDate).ToList() : data.OrderBy(p => p.AvailDate).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.FullName).ToList() : data.OrderBy(p => p.FullName).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Email).ToList() : data.OrderBy(p => p.Email).ToList();
                        break;
                    case "3":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.OrderCode).ToList() : data.OrderBy(p => p.OrderCode).ToList();
                        break;

                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.AvailDate).ToList() : data.OrderByDescending(p => p.AvailDate).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {

                Console.Write(ex);
            }
            return lst;
        }
    }

    public class WarrantyVM
    {
        public int WarrantyId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string OrderCode { get; set; }
        public bool? IsLikePurchase { get; set; }
        public string PurchaseLike { get; set; }
        public DateTime? AvailDate { get; set; }
    }
}
