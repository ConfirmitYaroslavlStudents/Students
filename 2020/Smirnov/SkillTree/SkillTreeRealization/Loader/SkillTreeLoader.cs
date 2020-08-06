using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using SkillTree.Graph;
using SkillTree;

namespace SkillTreeRealization.Loader
{
    public class SkillTreeLoader
    {
        private const string _folderDiscipline = "Discipline";
        private const int _startNameDiscipline = 11;
        private const int _endNameDiscipline = 16;
        public static List<Discipline> LoadDisciplines()
        {
            var disciplines = new List<Discipline>();
            Directory.CreateDirectory(_folderDiscipline);
            var files = Directory.GetFiles(_folderDiscipline);
            
            foreach (var file in files)
            {
                disciplines.Add(new Discipline(file.Substring(_startNameDiscipline, file.Length - _endNameDiscipline),
                    JsonSerializer.Deserialize<Graph>(File.ReadAllText(file))));
            }
            return disciplines;
        }
        public static void SaveDisciplines(List<Discipline> disciplines)
        {
            Directory.CreateDirectory(_folderDiscipline);
            foreach (var discipline in disciplines)
            {
                var jsonString = JsonSerializer.Serialize(discipline.Graph);
                File.WriteAllText(ReturnPath(discipline.Name, _folderDiscipline), jsonString);
            }
                          
        }
        private static string ReturnPath(string fileName, string folderName)
        {
            return folderName + @"\" + fileName + ".json";
        }
    }
}
