using System;
using System.Collections.Generic;
using System.Text;
using SkillTree;
using SkillTree.Graph;

namespace SkillTree
{
    public interface IDisciplineContainer
    {
        public void CreateNewDiscipline(string nameDiscipline);
        public void AddRequirementForDiscipline(Discipline discipline, Skill skill);
        public void AddRequirementForSkill(Discipline discipline, Skill firstSkill, Skill secondSkill);

        public IEnumerable<Skill> GetAllSkillsForDiscipline(Discipline discipline);
        public Vertex GetSkill(Discipline discipline, string nameSkill);
        public List<Discipline> GetAllDisciplines();
        public Discipline GetDiscipline(string nameDiscipline);
        public IEnumerable<Skill> GetAllWayUptoSkill(Discipline discipline, string nameSkill);
        public int GetAllTimeUptoSkill(Discipline discipline, string nameSkill);
    }
}
