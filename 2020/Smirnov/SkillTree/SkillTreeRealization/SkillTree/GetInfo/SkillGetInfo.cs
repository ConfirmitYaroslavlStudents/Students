using System;
using System.Collections.Generic;
using SkillTree.Graph;
using SkillTree;

namespace SkillTreeRealization.SkillTree.GetInfo
{
    public class SkillGetInfo
    {
        public static string ReturnAllInformationAboutSkill(string nameDiscipline, string nameSkill, List<Discipline> disciplines)
        {
            foreach (var discipline in disciplines)
            {
                if (discipline.Name == nameDiscipline)
                {
                    var vertex = discipline.Graph.FindVertex(nameSkill);
                    return $"Name = {vertex.Skill.Name} Difficult = {vertex.Skill.Difficult} Requirements =" + ReturnSkillRequirements(vertex) +
                $"Specification = {vertex.Skill.Specification} Time = {vertex.Skill.Time}";
                }
            }
            throw new InvalidOperationException("Discipline not found");
        }
        private static string ReturnSkillRequirements(Vertex vertex)
        {
            var requirements = "";
            foreach (var edge in vertex.Edges)
            {
                requirements += $"{edge.ConnectedVertex.Skill.Name} ";
            }

            return requirements;
        }
    }
}
