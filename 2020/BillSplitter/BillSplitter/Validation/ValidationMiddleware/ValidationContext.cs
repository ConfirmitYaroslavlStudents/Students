using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BillSplitter.Validation.ValidationMiddleware
{
    public class ValidationContext
    {
        public HttpContext HttpContext { get; }
        public Endpoint Endpoint { get; }
        public IEnumerable<string> Roles { get; }

        public ValidationContext(
            HttpContext context, 
            Endpoint endpoint, 
            IEnumerable<string> roles)
        {
            HttpContext = context;
            Endpoint = endpoint;
            Roles = roles;
        }
    }
}