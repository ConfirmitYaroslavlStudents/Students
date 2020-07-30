using SkillTree.Graph;
using System.Collections.Generic;

namespace UserController
{
    public class UserController //NotEnd
    {
        public static void LearnNewSkill(Skill skill, User.User user)
        {

        }
        private static bool ConteinsLearnedSkill(Skill skill, User.User user)
        {
            foreach(var learnedSkill in user.LearnedSkills)
            {
                if(learnedSkill.Name == skill.Name)
                {
                    return true;
                }
            }
            return false;
            
        }
        public static void LearnNewDiscipline(string nameDiscipline, Dictionary<string,Graph> disciplines, User.User user)
        {
            //user.LearnedDisciplines.Add();
        }
    }
}
