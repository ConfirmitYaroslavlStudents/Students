using System;
using System.Collections.Generic;
using SkillTree;

namespace SkillTreeBoard
{
    class ModeratorMenu
    {
        private readonly IManager _manager;

        public ModeratorMenu(IManager manager)
        {
            _manager = manager;
        }

        public void RunMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"User name : Admin\n" +
                                  $"1)GlobalCash.\n" +
                                  $"2)Add Discipline.\n" +
                                  $"3)Del Discipline.\n" +
                                  $"4)Sign out.\n" +
                                  $"Enter the line number : ");
                if (!int.TryParse(Console.ReadLine(), out var num)) continue;
                var exitFlag = false;
                switch (num)
                {
                    case 1:
                    {
                        var discipline = PrintMyDisciplines(_manager.GetAllDisciplines());
                        if (discipline == null)
                            continue;
                        new DisciplineEditMenu(_manager,discipline).RunMenu();
                        break;
                    }
                    case 2:
                    {
                        Console.Clear();
                        Console.Write("Enter Discipline's name :");
                        var name = Console.ReadLine();
                        if (_manager.ContainsDiscipline(name))
                        {
                            Console.WriteLine("Discipline exist.\nPress to continue.");
                            Console.ReadKey();
                        }
                        else if (string.Equals("exit", name))
                        {
                            Console.WriteLine("Incorrect name.\nPress to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            _manager.AddDisciplines(new Discipline(name));
                            Console.WriteLine($"Discipline \"{name}\" was added");
                            Console.WriteLine("Press to continue.");
                            Console.ReadKey();
                        }
                        break;
                    }
                    case 3:
                    {
                        Console.Clear();
                        Console.Write("Enter Discipline's name :");
                        var name = Console.ReadLine();
                        if (!_manager.ContainsDiscipline(name))
                        {
                            Console.WriteLine("Discipline not exist.\nPress to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            _manager.DeleteDisciplines(new Discipline(name));
                            Console.WriteLine($"Discipline \"{name}\" was deleted");
                            Console.WriteLine("Press to continue.");
                            Console.ReadKey();
                        }
                        break;
                    }
                    case 4: exitFlag = true; break;
                }
                if (exitFlag)
                    break;
            }

        }

        private Discipline PrintMyDisciplines(Dictionary<string, Discipline> disciplines)
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
