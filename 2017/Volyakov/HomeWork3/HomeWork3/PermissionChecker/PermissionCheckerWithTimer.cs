
namespace HomeWork3
{
    class PermissionCheckerWithTimer:Timer, IPermissionChecker
    {
        private PermissionChecker _permissionChecker = new PermissionChecker();
        
        public bool CheckPermission(MP3File file, User user)
        {
            timer.Start();
            var result = _permissionChecker.CheckPermission(file, user);
            timer.Stop();
            time += timer.ElapsedMilliseconds;
            return result;
        }
    }
}
