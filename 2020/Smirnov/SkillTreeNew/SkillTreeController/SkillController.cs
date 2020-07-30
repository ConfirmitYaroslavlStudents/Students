using System;
using System.Collections.Generic;
using SkillTree.Graph;

namespace SkillTreeContorller
{
    public class SkillController
    {
        public static void AddRequirementForSkill(string nameDiscipline, string firstSkill, string secondSkill, Dictionary<string, Graph> disciplines)
        {
            if (!disciplines.ContainsKey(nameDiscipline))
            {
                throw new InvalidOperationException("Discipline not found");
            }
            disciplines[nameDiscipline].AddEdge(firstSkill, secondSkill);
        }
        public static string ReturnAllInformationAboutSkill(string nameDiscipline, string nameSkill, Dictionary<string, Graph> disciplines)
        {
            if (!disciplines.ContainsKey(nameDiscipline))
            {
                throw new InvalidOperationException("Discipline not found");
            }
            var vertex = disciplines[nameDiscipline].FindVertex(nameSkill);

            return $"Name = {vertex.Skill.Name} Difficult = {vertex.Skill.Difficult} Requirements =" + ReturnSkillRequirements(vertex) +
                $"Specification = {vertex.Skill.Specification} Time = {vertex.Skill.Time}";
        }
        private static string ReturnSkillRequirements(Vertex vertex)
        {
            string Requirements = "";
            foreach (var edge in vertex.Edges)
            {
                Requirements += $"{edge.ConnectedVertex.Skill.Name} ";             
            }

            return Requirements;
        }
    }
}
