using System.Collections.Generic;
using SkillTree;

namespace SkillTreeBoard
{
    public interface ISkillTreeManager:IEnumerable<Discipline>
    {
        bool IsUserDisciplinesOnFocus { get; set; }

        Graph<Discipline> DisciplinesGraph { get; }

        void SaveCondition();

        void DownloadGraph(Discipline discipline);

        Graph<T> LoadGraph<T>(string name);

        void SaveGraph<T>(string name, Graph<T> graph);

        void DeleteGraph(string name);
    }
}