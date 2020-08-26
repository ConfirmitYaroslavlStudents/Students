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

        public async Task<ExternalUserInfo> GetExternalUserInfo(string scheme)
        {
            var result = await _contextAccessor.HttpContext.AuthenticateAsync(scheme);

            if (result?.Succeeded != true)
                throw new Exception("External authentication error");

            var externalUser = result.Principal;

            if (externalUser == null)
                throw new Exception("External authentication error");

            await _contextAccessor.HttpContext.SignOutAsync("Cookies");

            var givenName = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName).Value;

            var surname = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname).Value;

            var name = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            return new ExternalUserInfo
            {
                Provider = scheme,
                Name = name,
                Surname = surname,
                GivenName = givenName
            };
        }

        public void ExternalSignIn(ExternalUserInfo externalUser)
        {
            var user = _unitOfWork.Users.GetByName(externalUser.Provider, externalUser.Name);

            if (user == null)
                user = CreateNewUser(externalUser);

            SingIn(user);
        }

        public User CreateNewUser(ExternalUserInfo externalUser)
        {
            var newUser = new User
            {
                Provider = externalUser.Provider,
                Name = externalUser.Name,
                Surname = externalUser.Surname,
                GivenName = externalUser.GivenName
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