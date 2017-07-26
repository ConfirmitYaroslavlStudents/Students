using System;

namespace RenamersLib
{
    public class PermissionChecker : IPermissionChecker
    {
        public bool CheckPermissions(Mp3File file, UserRole role)
        {
            return role > (int)UserRole.Guest;
        }
    }
}
