using SkillTree.Graph;
using SkillTree;
using System;
using System.Collections.Generic;

namespace SkillTreeRealization
{
    public class UserController
    {
        public static void LearnNewSkill(string nameDiscipline, string nameSkill, List<Discipline> disciplines, User.User user)
        {
            var vertex = disciplines[GetIndexDiscipline(nameDiscipline, disciplines)].Graph.FindVertex(nameSkill);

            if(ConteinsLearnedSkill(vertex.Skill, user))
            {
                throw new InvalidOperationException("skill has already been learned");
            }

            foreach(var requirement in vertex.Edges)
            {
                if (!ConteinsLearnedSkill(requirement.ConnectedVertex.Skill, user))
                {
                    throw new InvalidOperationException("Not all requirements are met");
                }
            }

            user.LearnedSkills.Add(vertex.Skill);
        }

        private static int GetIndexDiscipline(string nameDiscipline, List<Discipline> disciplines)
        {
            for (int i = 0; i < disciplines.Count; i++)
            {
                if (disciplines[i].Name == nameDiscipline)
                {
                    return i;
                }
            }
            throw new InvalidOperationException("Discipline not found");
        }
        private static bool ConteinsLearnedSkill(Skill skill, User.User user)
        {
            foreach (var learnedSkill in user.LearnedSkills)
            {
                if (learnedSkill.Name == skill.Name)
                {
                    return true;
                }
            }
            return false;

        }
        public static void LearnNewDiscipline(string nameDiscipline, List<Discipline> disciplines, User.User user)
        {
            var Vertices = disciplines[GetIndexDiscipline(nameDiscipline, disciplines)];

            foreach (var vertex in Vertices.Graph.Vertices)
            {
                if (!ConteinsLearnedSkill(vertex.Skill, user))
                {
                    throw new InvalidOperationException("Not all requirements are met");
                }
            }

            user.LearnedDisciplines.Add(Vertices.Name);
        }
    }
    
}
