using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BillSplitter.Oauth
{
    public class VKOAuthHandler : OAuthHandler<VKOAuthOptions>
    {
        public VKOAuthHandler(
             IOptionsMonitor<VKOAuthOptions> options,
             ILoggerFactory logger,
             UrlEncoder encoder,
             ISystemClock clock)
             : base(options, logger, encoder, clock) { }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(
            ClaimsIdentity identity,
            AuthenticationProperties properties,
            OAuthTokenResponse tokens)
        {
            string address = QueryHelpers.AddQueryString(Options.UserInformationEndpoint, new Dictionary<string, string>
            {
                ["access_token"] = tokens.AccessToken,
                ["v"] = !string.IsNullOrEmpty(Options.ApiVersion) ? Options.ApiVersion : "5.78"
            });

            if (Options.Fields.Count != 0)
            {
                address = QueryHelpers.AddQueryString(address, "fields", string.Join(",", Options.Fields));
            }

            using var response = await Backchannel.GetAsync(address, Context.RequestAborted);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Proccess error");
            }

            using var container = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            using var enumerator = container.RootElement.GetProperty("response").EnumerateArray();
            var payload = enumerator.First();

            var principal = new ClaimsPrincipal(identity);
            var context = new OAuthCreatingTicketContext(principal, properties, Context, Scheme, Options, Backchannel, tokens, payload);

            context.RunClaimActions();

            await Options.Events.CreatingTicket(context);

            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }
    }
}
