using System;
using System.Collections.Generic;
using SkillTree.Graph;
using SkillTree;

namespace SkillTreeRealization.SkillTree.Contorller
{
    public class DisciplineController
    {
        public static void CreateNewDiscipline(string nameDiscipline, List<Discipline> disciplines)
        {
            foreach (var discipline in disciplines)
            {
                if (discipline.Name == nameDiscipline)
                {
                    throw new InvalidOperationException("Discipline has already been created");
                }
            }
            
            disciplines.Add(new Discipline(nameDiscipline));
        }
        public static void AddRequirementForDiscipline(string nameDiscipline, Skill skill, List<Discipline> disciplines)
        {
            foreach (var discipline in disciplines)
            {
                if (discipline.Name == nameDiscipline)
                {
                    discipline.Graph.AddVertex(skill);
                    return;
                }
            }
            throw new InvalidOperationException("Discipline not found");
        }
    }
}
