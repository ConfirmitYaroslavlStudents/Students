using System;

namespace ProcessorsLib
{
    public class PermissionChecker
    {
        public bool CheckPermissions(Mp3File file, UserRole role)
        {
            return (int)role >= (new Random()).Next((int)UserRole.Administrator + 1);
        }
    }
}
