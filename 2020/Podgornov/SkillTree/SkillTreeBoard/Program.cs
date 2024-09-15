using System;
using System.Collections.Generic;

namespace SkillTreeBoard
{
    public class Program
    {
        public static void RunMenu(IManager manager)
        {
            
            while (true)
            {
                Console.Clear();
                Console.Write("nick \"admin\" is moderator's name.\n" +
                              "1) Sign In.\n" +
                                  "2)Create new profile.\n" +
                                  "3)Exit.\n" +
                                  "Enter the line number : ");
                if (!int.TryParse(Console.ReadLine(), out var num)) continue;
                var exitFlag = false; 
                switch (num)
                {
                    case 1: SignInMenu(manager);break;
                    case 2: AddUserMenu(manager);break;
                    case 3: exitFlag = true;break;
                }
                if(exitFlag)
                    break;
            }
        }

        private static bool Check()
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Write 'R' if you want to repeat.");
            var flag = Console.ReadKey().Key == ConsoleKey.R;
            Console.Clear();
            return flag;

        }

        private static void AddUserMenu(IManager manager)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter your Name :");
                var name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    if (Check()) continue;
                    return;
                }

                var correctName = true;
                for (int i = 0; i < name.Length; i++)
                {
                    if (!char.IsLetterOrDigit(name[i]))
                        correctName = false;
                }

                if (!correctName)
                {
                    if (Check()) continue;
                    return;
                }

                if (!manager.AddUser(name))
                {
                    Console.WriteLine("User already exist.\nPress to continue.");
                    Console.ReadKey();
                    if (Check()) continue;
                    return;
                }
                Console.WriteLine($"User \"{name}\" was added. ");
                Console.WriteLine("Press to continue");
                Console.ReadKey();
                return;
            }
        }

        private static void SignInMenu(IManager manager)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter your name :");
                var name = Console.ReadLine();
                try
                {
                    if (string.Equals(name,"admin"))
                    {
                        new ModeratorMenu(manager).RunMenu();
                    }
                    else
                    {
                        var user = manager.GetUserByName(name);
                        new UserMenu(user).RunMenu();
                    }
                    return;
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("User not found.\nPress to continue");
                    Console.ReadKey();
                    if (Check()) continue;
                    return;
                }
            }
        }

    }
}
