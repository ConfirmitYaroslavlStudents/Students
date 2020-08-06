using System;
using System.Collections.Generic;
using System.Text;

namespace SkillTreeRealization
{
    class UserGetInfo
    {
        public static string ReturnNameAllLearnedSkills(User.User user)
        {
            var result = "";
            foreach(var learnedSkill in user.LearnedSkills)
            {
                result += $"{learnedSkill.Name} ";
            }
            return result;
        }
        public static string ReturnNameAllLearnedDisciplines(User.User user)
        {
            var result = "";
            foreach (var learnedDiscipline in user.LearnedDisciplines)
            {
                result += $"{learnedDiscipline} ";
            }
            return result;
        }
    }
}
