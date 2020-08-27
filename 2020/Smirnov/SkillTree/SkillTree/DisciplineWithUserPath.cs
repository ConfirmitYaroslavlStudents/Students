using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillTree.Graph;

namespace SkillTree
{
    public class DisciplineWithUserPath: IDisciplineContainer
    {
        private DisciplineContainer _decorated;
        public User _user;

        public DisciplineWithUserPath(DisciplineContainer decorated, User user)
        {
            _decorated = decorated;
            _user = user;
        }

        public void AddRequirementForDiscipline(Discipline discipline, Skill skill)
        {
            ((IDisciplineContainer)_decorated).AddRequirementForDiscipline(discipline, skill);
        }

        public void AddRequirementForSkill(Discipline discipline, Skill firstSkill, Skill secondSkill)
        {
            ((IDisciplineContainer)_decorated).AddRequirementForSkill(discipline, firstSkill, secondSkill);
        }

        public void CreateNewDiscipline(string nameDiscipline)
        {
            ((IDisciplineContainer)_decorated).CreateNewDiscipline(nameDiscipline);
        }

        public List<Discipline> GetAllDisciplines()
        {
            return ((IDisciplineContainer)_decorated).GetAllDisciplines();
        }

        public IEnumerable<Skill> GetAllSkillsForDiscipline(Discipline discipline)
        {
            return ((IDisciplineContainer)_decorated).GetAllSkillsForDiscipline(discipline);
        }

        public int GetAllTimeUptoSkill(Discipline discipline, string nameSkill) 
        {
            return ((IDisciplineContainer)_decorated).GetAllTimeUptoSkill(discipline, nameSkill);
        }

        public IEnumerable<Skill> GetAllWayUptoSkill(Discipline discipline, string nameSkill) // Переделал под юзера
        {
            var skills = (((IDisciplineContainer)_decorated).GetAllWayUptoSkill(discipline, nameSkill)).ToList(); // собрал весь путь
            var removedSkill = new Skill();

            foreach(var learnedSkill in _user.LearnedSkills) // удалил уже изученное
            {
                foreach(var skill in skills)
                {
                    if (learnedSkill.Name == skill.Name)
                    {
                        removedSkill = skill;
                    }
                }
                skills.Remove(removedSkill);
            }

            return skills; // вывел новый путь
        }

        public Discipline GetDiscipline(string nameDiscipline)
        {
            return ((IDisciplineContainer)_decorated).GetDiscipline(nameDiscipline);
        }

        public Vertex GetSkill(Discipline discipline, string nameSkill)
        {
            return ((IDisciplineContainer)_decorated).GetSkill(discipline, nameSkill);
        }
        public void ChangeUser()
        {

        }
    }
}
