namespace TimeMeasurer
{
    public class WithPermissionDecorator : IMp3Renamer
    {
        private readonly IMp3Renamer _renamer;
        private readonly IPermissionsChecker _checker;
        private readonly UserRole _userRole;

        public WithPermissionDecorator(IMp3Renamer renamer, IPermissionsChecker checker, UserRole userRole)
        {
            _renamer = renamer;
            _checker = checker;
            _userRole = userRole;
        }

        public void Rename(Mp3File file)
        {
            if (_checker.CheckPermission(file, _userRole))
            {
                _renamer.Rename(file);
            }
        }
    }
}