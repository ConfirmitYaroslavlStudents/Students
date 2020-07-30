using System;
using System.Collections.Generic;
using SkillTree.Graph;
using SkillTreeContorller;

namespace SkillTreeRealization.Menu
{
    public class UserMenu // NotEnd
    {
        private const string _returnAllDisciplines = "1";
        private const string _returnAllRequirementForDiscipline = "2";
        private const string _returnAllInformationAboutSkill = "3";
        private const string _LearnNewSkill = "4";
        private const string _LearnNewDiscipline = "5";
        private const string _saveAndExit = "6";

        private static void WriteDisciplineMenu()
        {
            Console.WriteLine("1 Return all disciplines");
            Console.WriteLine("2 Return all requirements for discipline");
            Console.WriteLine("3 Return all information about skill");
            Console.WriteLine("4 Learn new skill");
            Console.WriteLine("5 Learn new disciplines");
            Console.WriteLine("6 Save and exit");
        }
        public static bool WorkWitchUserMenu(User.User user, Dictionary<string, Graph> disciplines)
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
                        Console.Write($"{discipline.Key} ");
                    }
                    Console.WriteLine();

                    break;
                case (_returnAllRequirementForDiscipline):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();

                    var NamesSkill = DisciplineController.ReturnNameAllScillsForDiscipline(nameDiscipline, disciplines);

                    foreach (var nameSkill in NamesSkill)
                    {
                        Console.WriteLine(nameSkill);
                    }

                    break;
                case (_returnAllInformationAboutSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write name of skill");

                    Console.WriteLine(SkillController.ReturnAllInformationAboutSkill(nameDiscipline, Console.ReadLine(), disciplines));

                    break;
                case (_LearnNewSkill):
                    Console.Clear();
                    Console.WriteLine("Write iformation about skill in format\"name difficult specification time\"");
                    string[] inputString = Console.ReadLine().Split();

                    UserController.UserController.LearnNewSkill(new Skill(inputString[0], inputString[1],
                        inputString[2], int.Parse(inputString[3])), user);

                    break;
                case (_LearnNewDiscipline):

                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();

                    UserController.UserController.LearnNewDiscipline(nameDiscipline, disciplines, user);

                    break;
                case (_saveAndExit):

                    Loader.UserLoader.SaveUser();

                    return false;
                default:
                    Console.WriteLine("Incorrect Choice");

                    return false;
            }
            return true;
        }
    }
}
