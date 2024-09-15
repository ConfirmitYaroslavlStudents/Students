using System.Collections.Generic;
using SkillTree;

namespace SkillTreeBoard
{
    public interface IUser
    {
        string Name { get; set; }

        Dictionary<string, Discipline> GetUsersDisciplines();

        Dictionary<string, Discipline> GetAllDisciplines();

        void Download(Discipline discipline);

        void DeleteDiscipline(Discipline discipline);

        void SaveUsersDiscipline(Discipline discipline, GraphStatus<Skill> graphStatus);

        GraphStatus<Skill> LearnDiscipline(Discipline discipline);
    }
}
