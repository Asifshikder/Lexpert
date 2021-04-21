using Lexpert.Models;
using Lexpert.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            if (signInManager.IsSignedIn(User))
            {
                return Redirect("/Manage/Home/Index");
            }
            returnUrl = returnUrl.Replace("%2F", "/");
            LoginViewModel login = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var finduser = await userManager.FindByEmailAsync(login.Email);
            if (finduser == null)
            {
                ModelState.AddModelError("IncorrectInput", "Username or Password is incorrect");
                return View(login);
            }
            var result = await signInManager.PasswordSignInAsync(finduser, login.Password, false, false);
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(login.ReturnUrl))
                {
                    return Redirect("/Manage/Home/Index");
                }
                return Redirect(login.ReturnUrl);
            }
            ModelState.AddModelError("IncorrectInput", "Username or Password is incorrect");
            return View(login);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (signInManager.IsSignedIn(User))
            {
                await signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout(User user)
        {
            if (signInManager.IsSignedIn(User))
            {
                await signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        private Task<User> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
    }
}
