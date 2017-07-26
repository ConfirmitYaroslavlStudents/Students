namespace RenamersLib
{
    public interface IPermissionChecker
    {
        bool CheckPermissions(Mp3File file, UserRole role);
    }
}
