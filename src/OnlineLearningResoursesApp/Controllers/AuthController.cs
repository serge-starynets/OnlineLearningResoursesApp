using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using OnlineLearningResoursesApp.Models;
using OnlineLearningResoursesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearningResoursesApp.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;


        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Plans", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }

            return View();
        }

        //
        // GET: /Auth/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "App");
                }
                else
                {
                    AddErrors(result);
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }

        //public async Task<ActionResult> UploadPhoto(HttpPostedFileBase file)
        //{
        //    if(file != null && file.ContentLength > 0)
        //    {
        //        var user = await GetCurrentUserAsync();
        //        var username = user.UserName;
        //        var fileExt = Path.GetExtension(file, FileName);
        //        var fnm = username + ".png";
        //        if(fileExt.ToLower().EndsWith(".png") || fileExt.ToLower().EndsWith(".jpg"))
        //        {
        //            var filePath = HostingEnvironment.MapPath("~/img") + fnm;
        //            var directory = new DirectoryInfo(HostingEnvironment.MapPath("~/img"));
        //            if(directory.Exists == false)
        //            {
        //                directory.Create();
        //            }
        //            ViewBag.FilePath = filePath.ToString();
        //            file.SaveAs(filePath);
        //        } 
        //    }
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
