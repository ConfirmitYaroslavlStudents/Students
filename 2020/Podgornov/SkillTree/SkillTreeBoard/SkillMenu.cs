using System;
using SkillTree;

namespace SkillTreeBoard
{
    class SkillMenu
    {
        private readonly Vertex<Skill> _vertex;

        public SkillMenu(Vertex<Skill> vertex)
        {
            _vertex = vertex;
        }

        public Skill RunMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("User name : admin\n" +
                                  "1)About skill.\n" +
                                  "2)Edit Description.\n" +
                                  "3)Edit Training Time.\n" +
                                  "4)Edit Complexity.\n" +
                                  "5)Dependencies\n" +
                                  "6)Back.\n" +
                                  "Enter the line number : ");
                if (!int.TryParse(Console.ReadLine(), out var num)) continue;
                var exitFlag = false;
                switch (num)
                {
                    case 1:
                    {
                        Console.WriteLine($"Skill id: {_vertex.Id}");
                        Console.WriteLine(_vertex.Value.ToString());
                        Console.ReadKey();
                        break;
                    }
                    case 2:
                        SetSkillDescription();break;
                    case 3:
                        SetSkillTrainingTime();break;
                    case 4:
                        SetSkillComplexity();break;
                    case 5:
                        PrintDependencies();break;
                    case 6:
                        exitFlag = true;break;
                }

                if (exitFlag)
                    return _vertex.Value;
            }
        }

        private void SetSkillComplexity()
        {
            Console.CursorVisible = true;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select the Complexity {0,1,2} :");
                var flag = int.TryParse(Console.ReadLine(), out var result);
                if (!flag || (result > 2 && result < 0))
                {
                    if (Check()) continue;
                    break;
                }
                _vertex.Value.Complexity = (SkillComplexity)(result);
                break;
            }
        }

        private void SetSkillDescription()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Description:");
            _vertex.Value.Description = Console.ReadLine();
        }

        private void SetSkillTrainingTime()
        {
            Console.CursorVisible = true;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Training Time:");
                var flag = int.TryParse(Console.ReadLine(), out var result);
                if (!flag)
                {
                    if (Check()) continue;
                    break;
                }

                try
                {
                    _vertex.Value.TrainingTime = (result);
                    break;
                }
                catch (ArgumentException)
                {
                    if (Check()) continue;
                    break;
                }
            }
        }

        private void PrintDependencies()
        {
            Console.Clear();
            foreach (var id in _vertex.Dependencies)
            {
                Console.WriteLine($"Skill id :{id}");
            }

            Console.ReadKey();
        }

        private bool Check()
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Write 'R' if you want to repeat.");
            var flag = Console.ReadKey().Key == ConsoleKey.R;
            Console.Clear();
            return flag;

        }
    }
}
