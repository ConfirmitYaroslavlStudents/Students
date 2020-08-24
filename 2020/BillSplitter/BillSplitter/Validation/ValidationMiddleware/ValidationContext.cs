using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BillSplitter.Validation.ValidationMiddleware
{
    public class ValidationContext
    {
        public HttpContext HttpContext { get; }
        public IEnumerable<string> Roles { get; }

        public ValidationContext(
            HttpContext context, 
            IEnumerable<string> roles)
        {
            HttpContext = context;
            Roles = roles;
        }
    }
}