using Abc.WebUI.Entities;
using Abc.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        UserManager<CustomIdentityUser> _userManager;
        RoleManager<CustomIdentityRole> _rolManager;
        SignInManager<CustomIdentityUser> _signInManager;

        public AccountController
            (
              UserManager<CustomIdentityUser> userManager,
              RoleManager<CustomIdentityRole> rolManager,
              SignInManager<CustomIdentityUser> signInManager
            )
        {
            _userManager = userManager;
            _rolManager = rolManager;
            _signInManager = signInManager;
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                CustomIdentityUser user = new CustomIdentityUser
                {
                    UserName = registerVM.UserName,
                    Email = registerVM.Email
                };
                IdentityResult result = _userManager.CreateAsync(user, registerVM.Password).Result;
                if (result.Succeeded)
                {
                    if (!_rolManager.RoleExistsAsync("Admin").Result)
                    {
                        CustomIdentityRole role = new CustomIdentityRole
                        {
                            Name = "Admin"
                        };
                        IdentityResult roleResult = _rolManager.CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "Bu Rolü ekleyemiyoruz");
                            return View(registerVM);
                        }
                    }
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(registerVM);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, loginVM.RememberMe, false).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("", "Geçersiz Login işlemi");
            }
            return View(loginVM);
        }

        
        public ActionResult LogOff()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Login");
        }

    }
}
