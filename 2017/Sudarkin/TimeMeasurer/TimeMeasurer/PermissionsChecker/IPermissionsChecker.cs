namespace TimeMeasurer
{
    public interface IPermissionsChecker
    {
        bool CheckPermission(Mp3File file, UserRole role);
    }
}