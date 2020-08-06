using System;
using System.Collections.Generic;
using SkillTree;

namespace SkillTreeRealization.SkillTree.Contorller
{
    public class SkillController
    {
        public static void AddRequirementForSkill(string nameDiscipline, string firstSkill, string secondSkill, List<Discipline> disciplines)
        {
            foreach (var discipline in disciplines)
            {
                if (discipline.Name == nameDiscipline)
                {
                    discipline.Graph.AddEdge(firstSkill, secondSkill);
                    return;
                }
            }
            throw new InvalidOperationException("Discipline not found");
        }
        
    }
}
