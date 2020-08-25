using System.IO;
using System.Text.Json;
using SkillTree;

namespace SkillTreeRealization.Loader
{
    public class UserLoader
    {
        private const string FolderUser = "User";
        public static User LoadUser()
        {
            Directory.CreateDirectory(FolderUser);
            var file = Directory.GetFiles(FolderUser);
            if (file.Length != 0)
            {
                return JsonSerializer.Deserialize<User>(File.ReadAllText(file[0]));
            }
            else
            {
                return new User();
            }
        }
        public static void SaveUser(User user)
        {
            Directory.CreateDirectory(FolderUser);
            var jsonString = JsonSerializer.Serialize(user);
            File.WriteAllText(Path.Combine(FolderUser, $"{user.Name}.json"), jsonString);
        }
    }
}
