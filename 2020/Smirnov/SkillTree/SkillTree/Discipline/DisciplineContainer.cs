using System;
using System.Collections.Generic;
using SkillTree.Graph;
using System.Linq;

namespace SkillTree
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
                if (discipline.Name.Equals(nameDiscipline, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new InvalidOperationException("Discipline has already been created");
                }
            }
            if(nameDiscipline == "")
            {
                throw new InvalidOperationException("Discipline name not correct");
            }
            Disciplines.Add(new Discipline(nameDiscipline));
        }
        public void AddRequirementForDiscipline(Discipline discipline, Skill skill)
        {
            discipline.Graph.AddVertex(skill);
        }
        public void AddRequirementForSkill(Discipline discipline, Skill firstSkill, Skill secondSkill)
        {
            discipline.Graph.AddEdge(firstSkill.Name, secondSkill.Name);
        }
        public IEnumerable<Skill> GetAllSkillsForDiscipline(Discipline discipline)
        {
            var vertices = discipline.Graph.ReturnAllVertices();

            return from vertex in vertices                       
                   select vertex.Skill;
        }

        public Vertex GetSkill(Discipline discipline, string nameSkill)
        {
            return discipline.Graph.TryGetVertex(nameSkill);
        }

        public List<Discipline> GetAllDisciplines()
        {
            return Disciplines; 
        }

        public Discipline GetDiscipline(string nameDiscipline)
        {
            return (from discipline in Disciplines
                    where discipline.Name.Equals(nameDiscipline, StringComparison.InvariantCultureIgnoreCase)
                    select discipline).First();
        }

        public IEnumerable<Skill> GetAllWayUptoSkill(Discipline discipline, string nameSkill)
        {
            var graph = discipline.Graph;

            return from vertex in graph.SearchPath(graph.TryGetVertex(nameSkill))
                   select vertex.Skill;
        }
        public int GetAllTimeUptoSkill(Discipline discipline, string nameSkill)
        {
            var graph = discipline.Graph;

            return (from vertex in graph.SearchPath(graph.TryGetVertex(nameSkill))
                    select vertex.Skill.Time).Sum();
        }
    }
}
