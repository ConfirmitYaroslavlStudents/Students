using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Models.ViewModels.LoginAndRegister;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    [Route("Account")]
    public class AccountController : BaseController
    {

        public AccountController(UnitOfWork db) : base(db)
        {
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            User user = Db.Users.GetByName("LoginProvider", model.Name);

            if (user == null)
                ModelState.AddModelError("Name", "No Such User");
            else
            {
                return await LoginConfirm(user, returnUrl);
            }

            return View(model);
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            User user = Db.Users.GetByName("LoginProvider", model.Name);

            if (user != null)
                ModelState.AddModelError("Name", "UserName is already used");
            else
            {
                User newUser = new User
                {
                    Name = model.Name,
                    GivenName = model.GivenName,
                    Surname = model.Surname
                };

                Db.Users.Add(newUser);
                Db.Save();

                return await LoginConfirm(newUser, returnUrl);
            }

            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id",user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, $"{user.Surname} {user.GivenName}")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        [HttpPost]
        [Route("ExternalLogin")]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var callbackUrl = Url.Action("ExternalLoginCallback", new { scheme = provider, returnUrl });

            var props = new AuthenticationProperties
            {
                RedirectUri = callbackUrl
            };

            return new ChallengeResult(provider, props);
        }

        [Route("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string scheme, string returnUrl)
        {
            var result = await HttpContext.AuthenticateAsync(scheme);

            if (result?.Succeeded != true)
                throw new Exception("External authentication error");

            var externalUser = result.Principal;

            if (externalUser == null)
                throw new Exception("External authentication error");

            var name = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            await HttpContext.SignOutAsync("Cookies");

            var user = Db.Users.GetByName(scheme, name);

            if (user != null)
                return await LoginConfirm(user, returnUrl);

            user = CreateNewUser(externalUser, scheme);
            return await RegisterExternalConfirm(user, returnUrl);
        }

        private User CreateNewUser(ClaimsPrincipal externalUser, string scheme)
        {
            var givenName = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName).Value;

            var surname = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname).Value;

            var name = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            return new User
            {
                Provider = scheme,
                Name = name,
                Surname = surname,
                GivenName = givenName
            };
        }

        [Route("LoginConfirm")]
        public async Task<IActionResult> LoginConfirm(User user, string returnUrl)
        {
            await Authenticate(user);

            if (returnUrl != null)
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Bills");
        }

        [Route("RegisterExternalConfirm")]
        public async Task<IActionResult> RegisterExternalConfirm(User newUser, string returnUrl)
        {
            Db.Users.Add(newUser);
            Db.Save();

            return await LoginConfirm(newUser, returnUrl);
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Bills");
        }
    }
}
