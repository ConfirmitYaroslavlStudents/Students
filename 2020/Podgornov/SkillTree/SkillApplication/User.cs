using System.Collections.Generic;
using SkillTree;
using SkillTreeBoard;

namespace SkillApplication
{
    class User:IUser
    {
        private readonly IManager _manager;

        public string Name { get; set; }

        public User(string name , IManager manager)
        {
            _manager = manager;
            Name = name;
        }

        public Dictionary<string, Discipline> GetUsersDisciplines()
        {
            return _manager.GetUsersDisciplines(this);
        }

        public Dictionary<string, Discipline> GetAllDisciplines()
        {
            return _manager.GetAllDisciplines();
        }

        public void Download(Discipline discipline)
        {
            _manager.DownloadDiscipline(this, discipline);
        }

        public void DeleteDiscipline(Discipline discipline)
        {
            _manager.DeleteDiscipline(this, discipline);
        }

        public void SaveUsersDiscipline(Discipline discipline,GraphStatus<Skill> graphStatus)
        {
            _manager.SaveUsersDiscipline(this, discipline, graphStatus);
        }

        public GraphStatus<Skill> LearnDiscipline(Discipline discipline)
        {
            return _manager.LearnDiscipline(this, discipline);
        }
    }
}
