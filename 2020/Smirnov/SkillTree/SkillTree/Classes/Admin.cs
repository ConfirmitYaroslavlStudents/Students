using System;
using System.Collections.Generic;

namespace SkillTree.Classes
{
    [Serializable]
    class Admin: Person
    {
        public Admin(string name) : base(name)
        {
            
        }

        public bool CreateNewDiscipline(string name, ref List<Discipline> Discipline) // первый вариант
        {
            var NewDiscipline = new Discipline(name);
            if (!Discipline.Contains(NewDiscipline))
            {
                Discipline.Add(NewDiscipline);
                return true;
            }
            return false;
        }
        public bool CreateNewDiscipline(string name, List<Skill> skils, ref List<Discipline> Discipline) // второй вариант
        {
            var NewDiscipline = new Discipline(name, skils);
            if (!Discipline.Contains(NewDiscipline))
            {
                Discipline.Add(NewDiscipline);
                return true;
            }
            return false;
        }
        public bool AddScillForDiscipline(string name, Skill skill, ref List<Discipline> Discipline)
        {
            int i = 0;

            for (i = 0; Discipline[i].Name != name; i++) ;
            if (i == Discipline.Count)
                return false;
            Discipline[i].AddSkill(skill);
            return true;

        }
    }
}
