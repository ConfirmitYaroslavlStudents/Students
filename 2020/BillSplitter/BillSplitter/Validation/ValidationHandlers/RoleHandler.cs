using System.Collections.Generic;
using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Validation.ValidationHandlers
{
    public class RoleHandler
    {
        public bool Handle(Member member, IEnumerable<string> roles)
        {
            return roles.Any(role => member.Role == role);
        }
    }
}