using System;
using SkillTree;
using SkillTreeRealization.SkillTree;

namespace SkillTreeRealization.Menu
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
                switch (mode)
                {
                    case (_DisciplineMenu):
                        while (DisciplineMenu.WorkWitchDisciplineMenu(disciplines)) ;

                        break;
                    case (_SkillMenu):
                        while (SkillMenu.WorkWitchSkillMenu(disciplines)) ;

                        break;
                    case (_UserMenu):
                        if (user.User == null)
                        {
                            Console.WriteLine("Write name new user");
                            user = new UserContainer(new User.User(Console.ReadLine()));
                        }
                        while (UserMenu.WorkWitchUserMenu(user, disciplines)) ;

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
