using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3
{
    interface IPermissionChecker
    {
        bool CheckPermission(MP3File file, User user);
    }
}
