using System;

namespace SkillTreeRealization.Menu
{
    public class Menu
    {
        private const string _disciplineMenu = "1";
        private const string _skillMenu = "2";
        private const string _userMenu = "3";
        static string SelectMenu()
        {
            Console.WriteLine("Select Menu");
            Console.WriteLine("1 Discipline redactor menu");
            Console.WriteLine("2 Skill redactor menu");
            Console.WriteLine("3 User menu");
            return Console.ReadLine();
        }
        public static void OpenMenu()
        {
            var mode = SelectMenu();
            var disciplines = Loader.SkillTreeLoader.LoadDisciplines();
            var user = Loader.UserLoader.LoadUser();                              
            switch (mode)
            {
                case (_disciplineMenu):
                    while (DisciplineMenu.WorkWitchDisciplineMenu(disciplines)) ;

                    break;
                case (_skillMenu):
                    while (SkillMenu.WorkWitchSkillMenu(disciplines)) ;

                    break;
                case (_userMenu):
                    if (user == null)
                    {
                        Console.WriteLine("Write name new user");
                        user = new User.User(Console.ReadLine());
                    }
                    while (UserMenu.WorkWitchUserMenu(user, disciplines)) ;

                    break;
                default:
                    Console.WriteLine("Incorrect choice");

                    break;
            }
        }
    }
}
