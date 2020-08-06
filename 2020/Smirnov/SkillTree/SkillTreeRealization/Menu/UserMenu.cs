using System;
using System.Collections.Generic;
using SkillTree;
using SkillTreeRealization.SkillTree.GetInfo;

namespace SkillTreeRealization.Menu
{
    public class UserMenu // NotEnd
    {
        private const string _returnAllDisciplines = "1";
        private const string _returnAllRequirementForDiscipline = "2";
        private const string _returnAllInformationAboutSkill = "3";
        private const string _learnNewSkill = "4";
        private const string _learnNewDiscipline = "5";
        private const string _returnNameAllLearnedSkill = "6";
        private const string _returnNameAllLearnedDisciplines = "7";
        private const string _saveAndExit = "8";

        private static void WriteDisciplineMenu()
        {
            Console.WriteLine("1 Return all disciplines");
            Console.WriteLine("2 Return all requirements for discipline");
            Console.WriteLine("3 Return all information about skill");
            Console.WriteLine("4 Learn new skill");
            Console.WriteLine("5 Learn new disciplines");
            Console.WriteLine("6 Return Name All Learned Skills");
            Console.WriteLine("7 Return Name All Learned Disciplines");
            Console.WriteLine("8 Save and exit");
        }
        public static bool WorkWitchUserMenu(User.User user, List<Discipline> disciplines)
        {
            WriteDisciplineMenu();
            var item = Console.ReadLine();
            var nameDiscipline = "";
            switch (item)
            {
                case (_returnAllDisciplines):
                    Console.Clear();
                    foreach (var discipline in disciplines)
                    {
                        Console.Write($"{discipline.Name} ");
                    }
                    Console.WriteLine();

                    break;
                case (_returnAllRequirementForDiscipline):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    try
                    {
                        Console.WriteLine(DisciplineGetInfo.ReturnNameAllSkillsForDiscipline(nameDiscipline, disciplines));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (_returnAllInformationAboutSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write name of skill");

                    try
                    {
                        Console.WriteLine(SkillGetInfo.ReturnAllInformationAboutSkill(nameDiscipline, Console.ReadLine(), disciplines));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (_learnNewSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write name of skill");

                    try
                    {
                        UserController.LearnNewSkill(nameDiscipline, Console.ReadLine(), disciplines, user);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (_learnNewDiscipline):

                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();

                    try
                    {
                        UserController.LearnNewDiscipline(nameDiscipline, disciplines, user);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
               case(_returnNameAllLearnedSkill):

                    Console.WriteLine(UserGetInfo.ReturnNameAllLearnedSkills(user));

                    break;
                case (_returnNameAllLearnedDisciplines):

                    Console.WriteLine(UserGetInfo.ReturnNameAllLearnedDisciplines(user));

                    break;
                case (_saveAndExit):

                    Loader.UserLoader.SaveUser(user);

                    return false;
                default:
                    Console.WriteLine("Incorrect Choice");

                    return false;
            }
            return true;
        }
    }
}
