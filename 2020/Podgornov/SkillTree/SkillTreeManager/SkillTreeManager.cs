using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SkillTree;
using SkillTreeBoard;

namespace SkillTreeManager
{
    public class SkillTreeManager:ISkillTreeManager
    {
        private const string UsersDisciplinesPath = "./User";

        private const string DisciplinesPath = "./Disciplines";

        public bool IsUserDisciplinesOnFocus { get; set; }

        public SkillTreeManager()
        {
            if (!Directory.Exists(DisciplinesPath))
                Directory.CreateDirectory(DisciplinesPath);
            if (!Directory.Exists(UsersDisciplinesPath))
                Directory.CreateDirectory((UsersDisciplinesPath));
            IsUserDisciplinesOnFocus = true;
        }

        private string GetPath(string name)
        {
            var path = IsUserDisciplinesOnFocus
                ? Path.Combine(UsersDisciplinesPath, name + ".json")
                : Path.Combine(DisciplinesPath, name + ".json");
            return path;
        }

        public Graph LoadGraph(string name)
        {
            var path = GetPath(name);
            if (File.Exists(path))
            {
                GraphInformation graphInformation;
                using (var reader = new StreamReader(path))
                {
                    graphInformation = JsonConvert.DeserializeObject<GraphInformation>(reader.ReadToEnd());
                }
                if (graphInformation == null)
                    throw new ArgumentNullException(nameof(graphInformation), "Failed to load the graph.");
                return graphInformation.BuildGraph();
            }
            else
                throw new FileNotFoundException("File not found.");
        }

        public void SaveGraph(string name, Graph graph)
        {
            var path = GetPath(name);
            var graphInformation = graph.GetGraphInformation();
            var result = JsonConvert.SerializeObject(graphInformation);
            using (var writer = new StreamWriter(path))
            {
                writer.Write(result);
            }
        }

        public void DownloadGraph(string name)
        {
            var temp = IsUserDisciplinesOnFocus;
            IsUserDisciplinesOnFocus = false;
            if (Contains(name))
            {
                var beginPath = GetPath(name);
                IsUserDisciplinesOnFocus = true;
                var endPath = GetPath(name);
                File.Copy(beginPath, endPath,true);
                IsUserDisciplinesOnFocus = temp;
            }
            else
            {
                IsUserDisciplinesOnFocus = temp;
                throw new FileNotFoundException("File not found.");
            }
        }

        public IEnumerable<string> GetNamesOfAllGraphs()
        {
            return IsUserDisciplinesOnFocus
                ? from path in Directory.GetFiles(UsersDisciplinesPath) select Path.GetFileNameWithoutExtension(path)
                : from path in Directory.GetFiles(DisciplinesPath) select Path.GetFileNameWithoutExtension(path);
        }

        public void DeleteGraph(string name)
        {
            var path = GetPath(name);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
                throw new FileNotFoundException("File not found.");
        }

        public bool Contains(string name)
        {
            var path = GetPath(name);
            return File.Exists(path);
        }
    }
}
