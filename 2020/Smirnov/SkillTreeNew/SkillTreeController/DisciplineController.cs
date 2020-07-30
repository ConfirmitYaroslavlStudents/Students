using System;
using System.Collections.Generic;
using SkillTree.Graph;

namespace SkillTreeContorller
{
    public class DisciplineController
    {
        public static void CreateNewDiscipline(string name, Dictionary<string, Graph> disciplines)
        {
            disciplines.Add(name, new Graph());
        }
        public static void AddRequirementForDiscipline(string nameDiscipline, Skill skill, Dictionary<string, Graph> disciplines)
        {
            if (!disciplines.ContainsKey(nameDiscipline))
            {
                throw new InvalidOperationException("Discipline not found");
            }
            disciplines[nameDiscipline].AddVertex(skill);
        }
        public static List<string> ReturnNameAllScillsForDiscipline(string nameDiscipline, Dictionary<string, Graph> disciplines)
        {
            if (!disciplines.ContainsKey(nameDiscipline))
            {
                throw new InvalidOperationException("Discipline not found");
            }
            var NamesSkill = new List<string>();
            var vertices = disciplines[nameDiscipline].ReturnAllVertices();
            foreach (var vertex in vertices)
            {
                NamesSkill.Add(vertex.Skill.Name);
            }

            return NamesSkill;
        }
    }
}
