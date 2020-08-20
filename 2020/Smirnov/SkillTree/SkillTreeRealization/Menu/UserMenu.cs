using System;
using SkillTreeRealization.SkillTree;

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
        public static bool WorkWitchUserMenu(UserContainer user, DisciplineContainer disciplines)
        {
            WriteDisciplineMenu();
            var item = Console.ReadLine();
            var nameDiscipline = "";         
            switch (item)
            {
                case (LearnNewSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write name of skill");
                    var nameSkill = Console.ReadLine();                 

                    try
                    {
                        user.LearnNewSkill(disciplines.GetDiscipline(nameDiscipline).Graph.FindVertex(nameSkill));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (LearnNewDiscipline):

                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();

                    try
                    {
                        user.LearnNewDiscipline(disciplines.GetDiscipline(nameDiscipline));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
               case(GetNameAllLearnedSkill):

                    Console.WriteLine(user.GetNameAllLearnedSkills());

                    break;
                case (GetNameAllLearnedDisciplines):

                    Console.WriteLine(user.GetNameAllLearnedDisciplines());

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
