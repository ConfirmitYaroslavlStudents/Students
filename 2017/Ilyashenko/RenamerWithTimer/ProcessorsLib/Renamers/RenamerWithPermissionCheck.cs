namespace RenamersLib
{
    public class RenamerWithPermissionCheck : IFileRenamer
    {
        private IFileRenamer _renamer;
        private IPermissionChecker _checker;
        private UserRole _role;

        public RenamerWithPermissionCheck(IFileRenamer renamer, IPermissionChecker checker, UserRole role)
        {
            _renamer = renamer;
            _checker = checker;
            _role = role;
        }

        public void Rename(Mp3File file)
        {
            if (_checker.CheckPermissions(file, _role))
                _renamer.Rename(file);
        }
    }
}
