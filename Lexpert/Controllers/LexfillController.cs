using Lexpert.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Controllers
{
    public class LexfillController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private RoleManager<IdentityRole> roleManager;

        public LexfillController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.message = "";
            if (roleManager.Roles.Count() == 0)
            {
                IdentityRole model = new IdentityRole();
                IdentityRole model1 = new IdentityRole();
                model.Name = "Admin";
                model1.Name = "Client";
                await roleManager.CreateAsync(model);
                await roleManager.CreateAsync(model1);
            }
            else
            {
                ViewBag.message = "Already configured.";
                return View();
            }
            var check = userManager.FindByEmailAsync("sysadmin@admin.com");
            if (check.Result == null)
            {
                Models.User user = new Models.User();
                user.PhoneNumber = "12323223";
                user.UserName = "Lexpert";
                user.Email = "admin@lexpert.us";
                var password = "admin00lex";
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            ViewBag.message = "Successfully configured.";

            return View();
        }
    }
}
