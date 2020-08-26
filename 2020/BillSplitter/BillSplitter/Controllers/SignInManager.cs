using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BillSplitter.Controllers
{
    public class SignInManager
    {
        private IHttpContextAccessor _contextAccessor;
        private UnitOfWork _unitOfWork;

        public SignInManager(IHttpContextAccessor contextAccessor, UnitOfWork unitOfWork)
        {
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async void SingIn(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id",user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, $"{user.Surname} {user.GivenName}")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _contextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
        
        public bool CheckPassword(User user, string password)
        {
            return user.Password.Equals(password);
        }

        public async Task<ClaimsPrincipal> GetExternalUserInfo(string scheme)
        {
            var result = await _contextAccessor.HttpContext.AuthenticateAsync(scheme);

            if (result?.Succeeded != true)
                throw new Exception("External authentication error");

            var externalUser = result.Principal;

            if (externalUser == null)
                throw new Exception("External authentication error");

            await _contextAccessor.HttpContext.SignOutAsync("Cookies");

            return externalUser;
        }

        public void ExternalSignIn(ClaimsPrincipal externalUser, string scheme)
        {
            var name = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var user = _unitOfWork.Users.GetByName(scheme, name);

            if (user == null)
                user = CreateNewUser(externalUser, scheme);

            SingIn(user);
        }

        public User CreateNewUser(ClaimsPrincipal externalUser, string scheme)
        {
            var givenName = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName).Value;

            var surname = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname).Value;

            var name = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var newUser = new User
            {
                Provider = scheme,
                Name = name,
                Surname = surname,
                GivenName = givenName
            };

            CreateNewUser(newUser);

            return newUser;
        }

        public void CreateNewUser(User newUser)
        {
            _unitOfWork.Users.Add(newUser);
            _unitOfWork.Save();
        }

        public async void SignOut()
        {
            await _contextAccessor.HttpContext.SignOutAsync();
        }
    }
}