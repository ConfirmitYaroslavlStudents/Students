using System.Collections.Generic;
using SkillTree;

namespace SkillTreeBoard
{
    public interface ISkillTreeManager
    {
        bool IsUserDisciplinesOnFocus { get; set; }

        void DownloadGraph(string name);

        Graph LoadGraph(string name);

        void SaveGraph(string name, Graph graph);

        IEnumerable<string> GetNamesOfAllGraphs();

        void DeleteGraph(string name);

        bool Contains(string name);
    }
}