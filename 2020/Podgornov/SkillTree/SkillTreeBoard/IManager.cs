using System.Collections.Generic;
using SkillTree;

namespace SkillTreeBoard
{
    public interface IManager
    {

        Dictionary<string, Discipline> GetUsersDisciplines(IUser user);

        Dictionary<string, Discipline> GetAllDisciplines();

        void DownloadDiscipline(IUser user, Discipline discipline);

        void DeleteDiscipline(IUser user, Discipline discipline);

        void AddDisciplines(Discipline discipline);

        void DeleteDisciplines(Discipline discipline);

        Graph<Skill> EditDiscipline(Discipline discipline);

        void SaveDiscipline(Discipline discipline, Graph<Skill> graph);

        bool AddUser(string name);

        void SaveUsersDiscipline(IUser user, Discipline discipline, GraphStatus<Skill> graphStatus);

        GraphStatus<Skill> LearnDiscipline(IUser user, Discipline discipline);

        IUser GetUserByName(string name);

        bool ContainsDiscipline(string name);
    } 
}
