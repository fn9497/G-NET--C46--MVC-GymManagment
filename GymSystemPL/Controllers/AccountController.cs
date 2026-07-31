using GymSystem.Controllers;
using GymSystemBLL.ViewModels.AccountViewModel;
using GymSystemDAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymSystemPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel model , CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(model);
            var user=await _userManager.FindByEmailAsync(model.Email);
            if(user ==null)
            {
                ModelState.AddModelError("Invalid Login", "Invalid Email or Password");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,false);
            if(result.Succeeded)
            { 
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(model);



        }
        [Authorize]
        [HttpPost]

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
