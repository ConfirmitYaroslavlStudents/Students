using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SkillTree;
using SkillTreeBoard;

namespace SkillApplication
{
    class Manager:IManager
    {
        private Dictionary<string,User> _users;

        private Dictionary<string, Discipline> _disciplinesInformation;

        private const string SkillTreeBasePath = "./SkillTreeBase";

        private const string UsersPath = "./SkillTreeBase/Users";

        private const string UsersInformationPath = "./SkillTreeBase/UsersInformation.json";

        private const string DisciplinesGlobalCashInformationPath = "./SkillTreeBase/DisciplinesGlobalCashInformation.json";

        private const string DisciplinesGlobalCash = "./SkillTreeBase/Disciplines";

        public Manager()
        {
            if (!Directory.Exists(SkillTreeBasePath))
            {
                Directory.CreateDirectory(SkillTreeBasePath);
                Directory.CreateDirectory(UsersPath);
                Directory.CreateDirectory(DisciplinesGlobalCash);
                _users = new Dictionary<string, User>();
                _disciplinesInformation = new Dictionary<string, Discipline>();
                SaveUsers();
                SaveDisciplines(DisciplinesGlobalCashInformationPath,_disciplinesInformation);
            }
            GetUsers();
            _disciplinesInformation = GetDisciplinesInformation(DisciplinesGlobalCashInformationPath);
        }

        public void SaveUsersDiscipline(IUser user, Discipline discipline,GraphStatus<Skill> graphStatus)
        {
            var path = UsersPath + $"/{user.Name}" + $"/{discipline.Name}.json";
            var result = JsonConvert.SerializeObject(graphStatus.VertexStatuses, Formatting.Indented);
            using (var writer = new StreamWriter(path))
            {
                writer.Write(result);
            }
        }

        public IUser GetUserByName(string name) => new User(_users[name].Name, this);

        public bool ContainsDiscipline(string name) => _disciplinesInformation.ContainsKey(name);

        private void SaveUsers()
        {
            var result = JsonConvert.SerializeObject(_users, Formatting.Indented);
            using (var writer = new StreamWriter(UsersInformationPath))
            {
                writer.Write(result);
            }
        }

        private void SaveDisciplines(string path,Dictionary<string,Discipline> disciplines)
        {
            var result = JsonConvert.SerializeObject(disciplines, Formatting.Indented);
            using (var writer = new StreamWriter(path))
            {
                writer.Write(result);
            }
        }

        private void GetUsers()
        {
            if (!File.Exists(UsersInformationPath))
                throw new FileNotFoundException("File not found.");

            Dictionary<string, User> users;
            using (var reader = new StreamReader(UsersInformationPath))
            {
                users = JsonConvert.DeserializeObject<Dictionary<string, User>>(reader.ReadToEnd());
            }
            _users = users ?? throw new ArgumentNullException(nameof(users), "Failed to load Users information.");
        }

        private Dictionary<string,Discipline> GetDisciplinesInformation(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.");

            Dictionary<string, Discipline> disciplines;
            using (var reader = new StreamReader(path))
            {
                disciplines = JsonConvert.DeserializeObject<Dictionary<string, Discipline>>(reader.ReadToEnd());
            }
            return disciplines ?? throw new ArgumentNullException(nameof(disciplines), "Failed to load Disciplines information.");
        }

        public void SaveDiscipline(Discipline discipline, Graph<Skill> graph)
        {

            var path = DisciplinesGlobalCash + $"/{discipline.Name}.json";
            var result = JsonConvert.SerializeObject((graph).ToArray(), Formatting.Indented);
            using (var writer = new StreamWriter(path))
            {
                writer.Write(result);
            }
            SaveDisciplines(DisciplinesGlobalCashInformationPath, _disciplinesInformation);
        }

        public bool AddUser(string name)
        {
            if (_users.ContainsKey(name))
                return false;
            if (string.Equals("admin", name))
                return false;
            CreateNewUser(name);
            SaveUsers();
            return true;
        }

        private void CreateNewUser(string name)
        {
            _users.Add(name, new User(name, this));
            var path = SkillTreeBasePath + $"/Users/{name}";
            Directory.CreateDirectory(path);
            path += "/UsersDisciplines.json";
            SaveDisciplines(path, new Dictionary<string, Discipline>());
        }

        public Dictionary<string,Discipline> GetUsersDisciplines(IUser user)
        {
            var path = SkillTreeBasePath + $"/Users/{user.Name}/UsersDisciplines.json";
            return GetDisciplinesInformation(path);
        }

        private void SaveUsersDisciplines(IUser user ,Dictionary<string, Discipline> disciplines)
        {
            var path = SkillTreeBasePath + $"/Users/{user.Name}/UsersDisciplines.json";
            var result = JsonConvert.SerializeObject(disciplines, Formatting.Indented);
            using (var writer = new StreamWriter(path))
            {
                writer.Write(result);
            }
        }

        public Dictionary<string, Discipline> GetAllDisciplines()
        {
            return _disciplinesInformation;
        }

        public void DownloadDiscipline(IUser user,Discipline discipline)
        {
            var usersDiscipline = GetUsersDisciplines(user);
            if (!usersDiscipline.ContainsKey(discipline.Name))
                usersDiscipline.Add(discipline.Name, discipline);
            var graph = EditDiscipline(discipline);
            var path = UsersPath+$"/{user.Name}" + $"/{discipline.Name}.json";
            var obj = new GraphStatus<Skill>(graph);
            var result = JsonConvert.SerializeObject(obj.VertexStatuses, Formatting.Indented);
            using (var writer = new StreamWriter(path))
            {
                writer.Write(result);
            }

            SaveUsersDisciplines(user, usersDiscipline);
        }

        public void DeleteDiscipline(IUser user, Discipline discipline)
        {
            var usersDiscipline = GetUsersDisciplines(user);
            usersDiscipline.Remove(discipline.Name);
            var path = UsersPath + $"/{user.Name}" + $"/{discipline.Name}.json";
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.");
            File.Delete(path);
            SaveUsersDisciplines(user, usersDiscipline);
        }

        public void AddDisciplines(Discipline discipline)
        {
            _disciplinesInformation.Add(discipline.Name, discipline);
            SaveDiscipline(discipline, new Graph<Skill>());
        }

        public void DeleteDisciplines(Discipline discipline)
        {
            _disciplinesInformation.Remove(discipline.Name);
            var path = DisciplinesGlobalCash + $"/{discipline.Name}.json";
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.");
            File.Delete(path);
            SaveDisciplines(DisciplinesGlobalCashInformationPath, _disciplinesInformation);
        }

        public Graph<Skill> EditDiscipline(Discipline discipline)
        {
            var path = DisciplinesGlobalCash + $"/{discipline.Name}.json";
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.");

            IEnumerable<Vertex<Skill>> vertices;
            using (var reader = new StreamReader(path))
            {
                vertices = JsonConvert.DeserializeObject<IEnumerable<Vertex<Skill>>
                >(reader.ReadToEnd());
            }

            return vertices == null
                ? throw new ArgumentNullException(nameof(vertices), "Failed to load discipline.")
                : new Graph<Skill>(vertices);
        }

        public GraphStatus<Skill> LearnDiscipline(IUser user, Discipline discipline)
        {
            var path = UsersPath + $"/{user.Name}" + $"/{discipline.Name}.json";
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found.");
            var graph = EditDiscipline(discipline);
            Dictionary<int,bool> vertexStatuses;
            using (var reader = new StreamReader(path))
            {
                vertexStatuses = JsonConvert.DeserializeObject<Dictionary<int, bool>>(reader.ReadToEnd());
            }

            return vertexStatuses == null
                ? throw new ArgumentNullException(nameof(vertexStatuses), "Failed to load discipline.")
                : new GraphStatus<Skill>(graph, vertexStatuses);
        }

    }
}
