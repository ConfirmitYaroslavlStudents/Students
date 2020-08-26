using System;
using System.Collections.Generic;

namespace BillSplitter.Attributes
{
    public class RequireRolesAttribute : Attribute
    {
        public IEnumerable<string> Roles { get; }

        public RequireRolesAttribute(string roles)
        {
            Roles = roles.Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}