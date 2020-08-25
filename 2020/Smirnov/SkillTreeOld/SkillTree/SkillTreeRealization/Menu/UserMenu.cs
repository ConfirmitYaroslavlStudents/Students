using System;
using System.Linq;
using SkillTree;

namespace SkillTreeRealization.Menu
{
    public class UserMenu
    {
        private const string LearnNewSkill = "1";
        private const string LearnNewDiscipline = "2";
        private const string GetNameAllLearnedSkill = "3";
        private const string GetNameAllLearnedDisciplines = "4";
        private const string SaveAndExit = "5";

        private static void WriteDisciplineMenu()
        {
            Console.WriteLine("1 Learn new skill");
            Console.WriteLine("2 Learn new disciplines");
            Console.WriteLine("3 Get Name All Learned Skills");
            Console.WriteLine("4 Get Name All Learned Disciplines");
            Console.WriteLine("5 Save and return");
        }
        public static bool WorkWithUserMenu(UserContainer user, DisciplineContainer disciplines)
        {
            Console.Clear();
            WriteDisciplineMenu();
            var item = Console.ReadLine();
            var chooseDiscipline = new Discipline();
            switch (item)
            {
                case (LearnNewSkill):
                    Console.Clear();
                    try
                    {                       
                        Console.WriteLine("Write name of discipline");
                        chooseDiscipline = disciplines.GetDiscipline(Console.ReadLine());
                        Console.WriteLine("Write name of skill");
                        var nameSkill = Console.ReadLine();

                        user.LearnNewSkill(chooseDiscipline.Graph.TryGetVertex(nameSkill));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message == "Sequence contains no elements" ? "Discipline not found" : ex.Message);
                        Console.ReadKey();
                    }

                    break;
                case (LearnNewDiscipline):

                    Console.Clear();
                    try
                    {
                        Console.WriteLine("Write name of discipline");
                        chooseDiscipline = disciplines.GetDiscipline(Console.ReadLine());

                        user.LearnNewDiscipline(chooseDiscipline);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message == "Sequence contains no elements" ? "Discipline not found" : ex.Message);
                        Console.ReadKey();
                    }

                    break;
               case(GetNameAllLearnedSkill):
                    Console.WriteLine(string.Join(" ", from skill in user.GetAllLearnedSkills()
                                                       select skill.Name));

                    break;
                case (GetNameAllLearnedDisciplines):
                    Console.WriteLine(string.Join(" ", user.GetNameAllLearnedDisciplines()));

                    break;
                case (SaveAndExit):
                    Loader.UserLoader.SaveUser(user.User);

                    return false;
                default:
                    Console.WriteLine("Incorrect Choice");

                    return false;
            }
            return true;
        }
    }
}
