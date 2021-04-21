using Lexpert.Helpers;
using Lexpert.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Controllers
{
    public class WarrantyController : Controller
    {
        private AppDbContext db;
        private IEmailSender emailSender;
        private IConfiguration configuration;

        public WarrantyController(AppDbContext db,IEmailSender emailSender,IConfiguration configuration)
        {
            this.db = db;
            this.emailSender = emailSender;
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Warranty warranty)
        {
            warranty.AvailDate = DateTime.UtcNow;
            db.Warranty.Add(warranty);
            db.SaveChanges();
            SendEmailToCustomer(warranty);
            ViewBag.Message = "Success";
            return View();
        }

        private void SendEmailToCustomer(Warranty warranty)
        {
            try
            {
                var template = db.MailBodyContent.FirstOrDefault();
                var mailset = db.EmailSetting.FirstOrDefault();
                var title = "Your recent Amazon purchase - hidden camera charger from Lexpert Tech LLC.";


                var body = "<p>Hello " + warranty.FullName + ",</p>" +
                    "" + template.GreetingHeader + "" +
                    "" + template.MiddleContent + "" +
                    "" + template.FooterContent + "";
                emailSender.Post(
                          subject: title,
                          body: body,
                          recipients: warranty.Email,
                          sender: configuration["AdminContact"]

                          );
                //emailSender.Post(title,body,warranty.Email,configuration["AdminContact"]);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
