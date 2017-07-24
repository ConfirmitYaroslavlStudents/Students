using System.Threading;

namespace HomeWork3
{
    public class PermissionChecker : IPermissionChecker
    {
        public bool CheckPermission(MP3File file, User user)
        {
            Thread.Sleep(3);
            return file.Permission <= user.Permission;
        }
    }
}
