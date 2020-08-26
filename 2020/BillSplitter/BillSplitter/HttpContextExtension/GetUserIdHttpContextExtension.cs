using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BillSplitter.HttpContextExtension
{
    public static class GetUserIdHttpContextExtension
    {
        public static int GetCurrentUserId(this HttpContext context)
        {
            return int.Parse(
                context
                    .User.Claims
                    .Where(c => c.Type == "Id")
                    .Select(c => c.Value)
                    .SingleOrDefault() ?? string.Empty);
        }
    }
}