using System;
using System.Collections.Generic;
using SkillTree.Graph;
using SkillTree;

namespace SkillTreeRealization.SkillTree.GetInfo
{
    public class DisciplineGetInfo
    {
        public static string ReturnNameAllSkillsForDiscipline(string nameDiscipline, List<Discipline> disciplines)
        {
            foreach (var discipline in disciplines)
            {
                if (discipline.Name == nameDiscipline)
                {
                    var namesSkill = "";
                    var vertices = discipline.Graph.ReturnAllVertices();
                    foreach (var vertex in vertices)
                    {
                        namesSkill+= $"{vertex.Skill.Name} ";
                    }

                    return namesSkill;
                }
            }

            throw new InvalidOperationException("Discipline not found");
        }
    }
}
