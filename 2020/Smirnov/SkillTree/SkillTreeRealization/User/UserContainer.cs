using System;
using System.Collections.Generic;
using SkillTree;
using System.Linq;
using SkillTree.Graph;
using System.Text;

namespace SkillTreeRealization
{
    public class UserContainer
    {
        public UserContainer(User.User user)
        {
            User = user;
        }

        public User.User User;

        public void LearnNewSkill(Vertex vertex)
        {
            if (ConteinsLearnedSkill(vertex.Skill, User))
            {
                throw new InvalidOperationException("skill has already been learned");
            }

            foreach (var requirement in vertex.Edges)
            {
                if (!ConteinsLearnedSkill(requirement.ConnectedVertex.Skill, User))
                {
                    throw new InvalidOperationException("Not all requirements are met");
                }
            }

            User.LearnedSkills.Add(vertex.Skill);
        }
        private bool ConteinsLearnedSkill(Skill skill, User.User user)
        {
            if ((from learnedSkill in user.LearnedSkills
                 where learnedSkill.Name == skill.Name
                 select learnedSkill).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void LearnNewDiscipline(Discipline discipline)
        {
            var Vertices = discipline;

            foreach (var vertex in Vertices.Graph.Vertices)
            {
                if (!ConteinsLearnedSkill(vertex.Skill, User))
                {
                    throw new InvalidOperationException("Not all requirements are met");
                }
            }

            User.LearnedDisciplines.Add(Vertices.Name);
        }
        public string GetNameAllLearnedSkills()
        {
            return string.Join(" ", (from skill in User.LearnedSkills
                                     select skill.Name));
        }
        public string GetNameAllLearnedDisciplines()
        {
            return string.Join(" ", (from discipline in User.LearnedDisciplines
                                     select discipline));
        }
    }
}
