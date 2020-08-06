using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using SkillTree.Graph;
using SkillTree;

namespace SkillTreeRealization.Loader
{
    public class UserLoader
    {
        private const string _folderUser = "User";
        private const int _startNameUser = 5;
        private const int _endNameUser = 10;
        public static User.User LoadUser()
        {
            Directory.CreateDirectory(_folderUser);
            var file = Directory.GetFiles(_folderUser);
            if (file.Length != 0)
            {
                return JsonSerializer.Deserialize<User.User>(File.ReadAllText(file[0]));
            }
            else
            {
                return null;
            }
        }
        public static void SaveUser(User.User user)
        {
            Directory.CreateDirectory(_folderUser);
            var jsonString = JsonSerializer.Serialize(user);
            File.WriteAllText(ReturnPath(user.Name, _folderUser), jsonString);
        }
        private static string ReturnPath(string fileName, string folderName)
        {
            return folderName + @"\" + fileName + ".json";
        }
    }
}
