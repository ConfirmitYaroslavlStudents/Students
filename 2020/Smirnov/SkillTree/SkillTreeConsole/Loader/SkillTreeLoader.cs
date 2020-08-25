using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SkillTree.Graph;
using SkillTree;

namespace SkillTreeConsole.Loader
{
    public class SkillTreeLoader
    {
        private const string FolderDiscipline = "Discipline";
        public static List<Discipline> LoadDisciplines()
        {
            var disciplines = new List<Discipline>();
            Directory.CreateDirectory(FolderDiscipline);
            var files = Directory.GetFiles(FolderDiscipline);

            foreach (var file in files)
            {
                disciplines.Add(new Discipline(Path.GetFileNameWithoutExtension(file),
                    JsonSerializer.Deserialize<Graph>(File.ReadAllText(file))));
            }
            return disciplines;
        }
        public static void SaveDisciplines(List<Discipline> disciplines)
        {
            
            Directory.CreateDirectory(FolderDiscipline);
            foreach (var discipline in disciplines)
            {
                var jsonString = JsonSerializer.Serialize(discipline.Graph);
                File.WriteAllText(Path.Combine(FolderDiscipline, $"{discipline.Name}.json"), jsonString);
            }
                          
        }
    }
}
