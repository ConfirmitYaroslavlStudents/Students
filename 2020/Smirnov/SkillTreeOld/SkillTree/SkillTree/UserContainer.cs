using System;
using SkillTree;
using System.Linq;
using SkillTree.Graph;
using System.Collections.Generic;

namespace SkillTreeRealization
{
    public class UserContainer
    {
        public UserContainer(User user)
        {
            User = user;
        }

        public User User { get; set; }

        public void LearnNewSkill(Vertex vertex)
        {
            if (User.LearnedSkills.Contains(vertex.Skill))
            {
                throw new InvalidOperationException("skill has already been learned");
            }

            foreach (var requirement in vertex.Edges)
            {
                if (!User.LearnedSkills.Contains(vertex.Skill))
                {
                    throw new InvalidOperationException("Not all requirements are met");
                }
            }

            User.LearnedSkills.Add(vertex.Skill);
        }
        public void LearnNewDiscipline(Discipline discipline)
        {
            var Vertices = discipline;

            foreach (var vertex in Vertices.Graph.Vertices)
            {
                if (!User.LearnedSkills.Contains(vertex.Skill))
                {
                    throw new InvalidOperationException("Not all requirements are met");
                }
            }

            User.LearnedDisciplines.Add(Vertices.Name);
        }
        public List<Skill> GetAllLearnedSkills()
        {
            return User.LearnedSkills;
                   
        }
        public List<string> GetNameAllLearnedDisciplines()
        {
            return User.LearnedDisciplines;
        }
    }
}
