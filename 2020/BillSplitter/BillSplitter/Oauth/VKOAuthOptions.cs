using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace BillSplitter.Oauth
{
    public class VKOAuthOptions : OAuthOptions
    {
        public VKOAuthOptions()
        {
            ClaimsIssuer = "Vkontakte";

            CallbackPath = "/signin-vkontakte";

            AuthorizationEndpoint = "https://oauth.vk.com/authorize";
            TokenEndpoint = "https://oauth.vk.com/access_token";
            UserInformationEndpoint = "https://api.vk.com/method/users.get.json";

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
            ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
        }
        public ISet<string> Fields { get; } = new HashSet<string>
        {
            "id",
            "first_name",
            "last_name",
        };

        public string ApiVersion { get; set; } = "5.78";
    }
}