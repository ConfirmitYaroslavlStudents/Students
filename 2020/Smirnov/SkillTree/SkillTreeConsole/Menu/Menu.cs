using System;
using SkillTree;

namespace SkillTreeConsole.Menu
{
    public class Menu
    {
        private const string _DisciplineMenu = "1";
        private const string _SkillMenu = "2";
        private const string _UserMenu = "3";
        private const string _SaveEndExit = "4";
        static string SelectMenu()
        {
            Console.Clear();
            Console.WriteLine("Select Menu");
            Console.WriteLine("1 Discipline redactor menu");
            Console.WriteLine("2 Skill redactor menu");
            Console.WriteLine("3 User menu");
            Console.WriteLine("4 Save and exit");
            return Console.ReadLine();
        }
        public static void OpenMenu()
        {
            var disciplines = new DisciplineContainer(Loader.SkillTreeLoader.LoadDisciplines());
            var user = new UserContainer(Loader.UserLoader.LoadUser());
            do
            {
                var mode = SelectMenu();
                Console.Clear();
                switch (mode)
                {
                    case (_DisciplineMenu):
                        while (DisciplineMenu.WorkWithDisciplineMenu(disciplines)) ;

                        break;
                    case (_SkillMenu):
                        try
                        {
                            Console.WriteLine("Write name of discipline");
                            var skillMenu = new SkillMenu(disciplines.GetDiscipline(Console.ReadLine()));
                            Console.Clear();
                            while (skillMenu.WorkWithSkillMenu(disciplines)) ;
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message == "Sequence contains no elements" ? "Discipline not found" : ex.Message);
                            Console.ReadKey();
                        }
                        
                        break;
                    case (_UserMenu):
                        if (user.User.Name == null)
                        {
                            Console.WriteLine("Write name new user");
                            user = new UserContainer(new User(Console.ReadLine()));
                        }
                        while (UserMenu.WorkWithUserMenu(user, new DisciplineWithUserPath(disciplines, user.User))) ; // передаю другой класс

                        break;
                    case (_SaveEndExit):
                        return;

                    default:
                        Console.WriteLine("Incorrect choice");

                        break;
                }
            }
            while (true);
        }
    }
}
