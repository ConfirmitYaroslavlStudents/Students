using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SkillTree;
using SkillTreeBoard;

namespace SkillApplication
{
    public class SkillTreeManager:ISkillTreeManager
    {
        private const string UsersDisciplinesPath = "./User";

        private const string DisciplinesPath = "./Disciplines";

        private const string DisciplineGraphName = "DisciplineGraph";

        private const string UserDisciplinesName = "UserDiscipline";

        public Graph<Discipline> DisciplinesGraph { get; }

        public Dictionary<int, Discipline> UsersDisciplines { get; private set; }

        public bool IsUserDisciplinesOnFocus { get; set; }

        public SkillTreeManager()
        {
            IsUserDisciplinesOnFocus = false;
            if (!Directory.Exists(DisciplinesPath))
            {
               
                Directory.CreateDirectory(DisciplinesPath);
                var disciplineGraph = new Graph<Discipline>();
                DisciplinesGraph = disciplineGraph;
                SaveCondition();
            }
            else
            {
                DisciplinesGraph = LoadGraph<Discipline>(DisciplineGraphName);
            }
            IsUserDisciplinesOnFocus = true;
            if (!Directory.Exists(UsersDisciplinesPath))
            {
                Directory.CreateDirectory((UsersDisciplinesPath));
                UsersDisciplines = new Dictionary<int, Discipline>();
                SetUserDiscipline();
            }
            else
            {
                UsersDisciplines = GetUserDiscipline();
            }
        }

        private string GetPath(string name)
        {
            var path = IsUserDisciplinesOnFocus
                ? Path.Combine(UsersDisciplinesPath, name + ".json")
                : Path.Combine(DisciplinesPath, name + ".json");
            return path;
        }

        public Graph<T> LoadGraph<T>(string name)
        {
            var path = GetPath(name);
            if (File.Exists(path))
            {
                IDictionary<int,Vertex<T>> vertexesDictionary;
                using (var reader = new StreamReader(path))
                {
                    vertexesDictionary = JsonConvert.DeserializeObject<IDictionary<int, Vertex<T>>>(reader.ReadToEnd());
                }
                if (vertexesDictionary == null)
                    throw new ArgumentNullException(nameof(vertexesDictionary), "Failed to load the graph.");
                return new Graph<T>(new ReadOnlyDictionary<int, Vertex<T>>(vertexesDictionary));
            }
            else
                throw new FileNotFoundException("File not found.");
        }

        public void SaveGraph<T>(string name, Graph<T> graph)
        {
            var path = GetPath(name);
            var result = JsonConvert.SerializeObject(graph.VertexesDictionary, Formatting.Indented);
            using (var writer = new StreamWriter(path))
            {
                writer.Write(result);
            }
        }

        public void DownloadGraph(Discipline discipline)
        {
            var temp = IsUserDisciplinesOnFocus;
            IsUserDisciplinesOnFocus = true;
            if (!UsersDisciplines.ContainsKey(discipline.Id))
                UsersDisciplines.Add(discipline.Id, discipline);

            IsUserDisciplinesOnFocus = false;
            var beginPath = GetPath(discipline.Id.ToString());
            IsUserDisciplinesOnFocus = true;
            var endPath = GetPath(discipline.Id.ToString());
            File.Copy(beginPath, endPath, true);
            SaveCondition();
            IsUserDisciplinesOnFocus = temp;
        }

        public void DeleteGraph(string name)
        {
            var path = GetPath(name);
            if (File.Exists(path))
            {
                File.Delete(path);
                if (IsUserDisciplinesOnFocus)
                {
                    UsersDisciplines.Remove(int.Parse(name));
                }
                else
                {
                    DisciplinesGraph.RemoveVertex(int.Parse(name));
                }

                SaveCondition();
            }
            else
                throw new FileNotFoundException("File not found.");
        }

        public void SaveCondition()
        {
            if (IsUserDisciplinesOnFocus)
                SetUserDiscipline();
            else
                SaveGraph(DisciplineGraphName, DisciplinesGraph);
        }

        public IEnumerator<Discipline> GetEnumerator()
        {
            return IsUserDisciplinesOnFocus ? UsersDisciplines.Values.GetEnumerator() : DisciplinesGraph.Select(i => i.Value).GetEnumerator();
        }

        private Dictionary<int,Discipline> GetUserDiscipline()
        {
            using (var reader = new StreamReader(GetPath(UserDisciplinesName)))
            {
                UsersDisciplines = JsonConvert.DeserializeObject<Dictionary<int,Discipline>>(reader.ReadToEnd());
            }
            if (UsersDisciplines == null)
                throw new ArgumentNullException(nameof(UsersDisciplines), "Failed to load the Users disciplines.");
            return UsersDisciplines;
        }

        private void SetUserDiscipline()
        {
            var result = JsonConvert.SerializeObject(UsersDisciplines, Formatting.Indented);
            using (var writer = new StreamWriter(GetPath(UserDisciplinesName)))
            {
                writer.Write(result);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
