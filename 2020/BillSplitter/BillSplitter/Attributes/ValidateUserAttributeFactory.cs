using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BillSplitter.Attributes
{
    public class ValidateUserAttributeFactory : Attribute, IFilterFactory
    {
        public string RequestedRole { get; set; }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = serviceProvider.GetService<ValidateUserAttribute>();
            filter.Role = RequestedRole;

            return filter;
        }

        public bool IsReusable => false;
    }
}