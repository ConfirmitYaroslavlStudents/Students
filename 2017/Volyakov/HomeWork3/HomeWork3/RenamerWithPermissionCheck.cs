
namespace HomeWork3
{
    public class RenamerWithPermissionCheck : IRenamer
    {
        private User _user;
        private IRenamer _renamer;

        public RenamerWithPermissionCheck(IRenamer renamer, User user)
        {
            _user = user;
            _renamer = renamer;
        }

        private bool CheckPermission(MP3File file, User user)
        {
            return file.Permission <= user.Permission;
        }

        public void Rename(MP3File file)
        {
            if (CheckPermission(file, _user))
                _renamer.Rename(file);
        }
    }
}
