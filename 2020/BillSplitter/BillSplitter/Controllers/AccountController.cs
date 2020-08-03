using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsersDbAccessor _usersAccessor;
        public AccountController(BillContext context)
        {
            _usersAccessor = new UsersDbAccessor(context);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            User user = _usersAccessor.GetUserByName(model.Name);
            if (user == null)
                ModelState.AddModelError("Name", "No Such User");
            else
            {
                await Authenticate(user);

                if (returnUrl != null)
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Bills");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            User user = _usersAccessor.GetUserByName(model.Name);

            if (user != null)
                ModelState.AddModelError("Name", "UserName is already used");
            else
            {
                User newUser = new User {Name = model.Name};
                _usersAccessor.AddUser(newUser);

                await Authenticate(newUser);

                if (returnUrl != null)
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Bills");
            }

            return View(model);
        }

        private async Task Authenticate(User user)
        {

            var claims = new List<Claim>
            {
                new Claim("Id",user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Bills");
        }
    }
}
