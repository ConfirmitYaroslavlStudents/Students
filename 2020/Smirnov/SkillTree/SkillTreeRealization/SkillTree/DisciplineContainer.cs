using System;
using System.Collections.Generic;
using SkillTree;
using SkillTree.Graph;
using System.Linq;

namespace SkillTreeRealization.SkillTree
{
    public class DisciplineContainer
    {
        public DisciplineContainer(List<Discipline> disciplines)
        {
            Disciplines = disciplines;
        }

        public List<Discipline> Disciplines;

        public void CreateNewDiscipline(string nameDiscipline)
        {
            foreach (var discipline in Disciplines)
            {
                if (discipline.Name == nameDiscipline)
                {
                    throw new InvalidOperationException("Discipline has already been created");
                }
            }

            Disciplines.Add(new Discipline(nameDiscipline));
        }
        public void AddRequirementForDiscipline(string nameDiscipline, Skill skill)
        {
            (from discipline in Disciplines
             where discipline.Name == nameDiscipline
             select discipline
             ).First().Graph.AddVertex(skill);
        }
        public void AddRequirementForSkill(string nameDiscipline, string firstSkill, string secondSkill)
        {
            (from discipline in Disciplines
             where discipline.Name == nameDiscipline
             select discipline
             ).First().Graph.AddEdge(firstSkill, secondSkill);
        }
        public string GetNameAllSkillsForDiscipline(string nameDiscipline)
        {
            var vertices = (from discipline in Disciplines
                            where discipline.Name == nameDiscipline
                            select discipline).First().Graph.ReturnAllVertices();

            return string.Join(" ", (from vertex in vertices                       
                                     select vertex.Skill.Name));
        }

        public string GetAllInformationAboutSkill(string nameDiscipline, string nameSkill)
        {
            var vertex = (from discipline in Disciplines
                          where discipline.Name == nameDiscipline
                          select discipline).First().Graph.FindVertex(nameSkill);

            return $"Name = {vertex.Skill.Name} Difficult = {vertex.Skill.Difficult} " +
                   $"Requirements =" + string.Join(" ", from edge in vertex.Edges
                                       select edge.ConnectedVertex.Skill.Name) +
                   $"Specification = {vertex.Skill.Specification} Time = {vertex.Skill.Time}";
        }

        public string GetAllDisciplines()
        {
            return string.Join(" ", (from discipline in Disciplines
                                     select discipline.Name)); 
        }

        public Discipline GetDiscipline(string nameDiscipline)
        {
            return (from discipline in Disciplines
                   where discipline.Name == nameDiscipline
                   select discipline).First();
        }

        public string GetAllWayUptoSkill(string nameDiscipline, string nameSkill)
        {
            var graph = GetDiscipline(nameDiscipline).Graph;

            return string.Join(" ", (from vertex in graph.BreadthFirstSearch(graph.FindVertex(nameSkill))
                                    select vertex.Skill.Name).Reverse());
        }
        public int GetAllTimeUptoSkill(string nameDiscipline, string nameSkill)
        {
            var graph = GetDiscipline(nameDiscipline).Graph;

            return (from vertex in graph.BreadthFirstSearch(graph.FindVertex(nameSkill))
                    select vertex.Skill.Time).Sum();
        }
    }
}
