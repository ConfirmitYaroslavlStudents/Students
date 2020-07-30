using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using SkillTree.Graph;

namespace SkillTreeRealization.Loader
{
    public class SkillTreeLoader
    {
        private const string _folderDiscipline = "Discipline";
        private const int _startNameDiscipline = 11;
        private const int _endNameDiscipline = 16;
        public static Dictionary<string, Graph> LoadDisciplines()
        {
            var disciplines = new Dictionary<string, Graph>();
            Directory.CreateDirectory(_folderDiscipline);
            string[] files = Directory.GetFiles(_folderDiscipline);
            
            foreach (var file in files)
            {
                disciplines.Add(file.Substring(_startNameDiscipline, file.Length - _endNameDiscipline), JsonSerializer.Deserialize<Graph>(File.ReadAllText(file)));
            }
            return disciplines;
        }
        public static void SaveDisciplines(Dictionary<string, Graph> disciplines)
        {
            Directory.CreateDirectory(_folderDiscipline);
            foreach (var discipline in disciplines)
            {
                var jsonString = JsonSerializer.Serialize(discipline.Value);
                File.WriteAllText(ReturnPath(discipline.Key, _folderDiscipline), jsonString);
            }
                          
        }
        private static string ReturnPath(string fileName, string folderName)
        {
            return folderName + @"\" + fileName + ".json";
        }
    }
}
