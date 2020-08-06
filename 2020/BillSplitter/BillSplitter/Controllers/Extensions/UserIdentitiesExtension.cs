using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers.Extensions
{
    public static class UserIdentitiesExtension
    {
        public static int GetUserId(this Controller controller)
        {
            return int.Parse(
                controller
                    .HttpContext
                    .User.Claims
                    .Where(c => c.Type == "Id")
                    .Select(c => c.Value)
                    .SingleOrDefault() ?? string.Empty);
        }

        public static string GetUserName(this Controller controller)
        {
            return controller.User.Identity.Name;
        }
    }
}