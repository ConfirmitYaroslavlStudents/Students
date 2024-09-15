using System;
using System.Collections.Generic;
using SkillTree;

namespace SkillTreeBoard
{
    class UserMenu
    {
        private readonly IUser _user;

        public UserMenu(IUser user)
        {
            _user = user;
        }

        public void RunMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"User name : {_user.Name}\n" +
                                  $"1)My Disciplines.\n" +
                                  $"2)GlobalCash\n" +
                                  $"3)Delete.\n" +
                                  $"4)Sign out.\n" +
                                  $"Enter the line number : ");
                if (!int.TryParse(Console.ReadLine(), out var num)) continue;
                var exitFlag = false;
                switch (num)
                {
                    case 1:
                    {
                        var discipline = PrintMyDisciplines(_user.GetUsersDisciplines());
                        if(discipline == null)
                            continue;
                        new DisciplineMenuForUser(_user, discipline).RunMenu();
                        break;
                    }
                    case 2:
                    {
                        Console.WriteLine("Discipline to Download.");
                        var discipline = PrintMyDisciplines(_user.GetAllDisciplines());
                        if (discipline == null)
                            continue;
                        _user.Download(discipline);
                        Console.WriteLine($"Discipline {discipline.Name} was downloaded.");
                        Console.ReadKey();
                        break;
                    }
                    case 4: exitFlag = true; break;
                    case 3:
                    {
                        var discipline = PrintMyDisciplines(_user.GetUsersDisciplines());
                        if (discipline == null)
                            continue;
                        _user.DeleteDiscipline(discipline);
                        Console.WriteLine($"Discipline {discipline.Name} was deleted.");
                        Console.ReadKey();
                            break;
                    }
                }
                if (exitFlag)
                    break;
            }
        }

        private Discipline PrintMyDisciplines(Dictionary<string,Discipline> disciplines)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"total : {disciplines.Count}");
                foreach (var discipline in disciplines.Keys)
                {
                    Console.WriteLine($"{discipline}");
                }
                Console.Write("Enter Discipline name to get information (or exit if you want to exit) : ");
                var name = Console.ReadLine();
                if (string.Equals(name, "exit"))
                    return null;
                if (string.IsNullOrEmpty(name))
                    continue;
                if (!disciplines.ContainsKey(name))
                {
                    Console.WriteLine("Discipline not exist.\nPress to Continue.");
                    Console.ReadKey();
                }
                else
                {
                    return disciplines[name];
                }
            }
        }
    }
}
