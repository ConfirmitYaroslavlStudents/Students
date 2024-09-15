using System;
using System.Collections.Generic;
using System.Linq;
using SkillTree;

namespace SkillTreeBoard
{
    class DisciplineEditMenu
    {
        private readonly IManager _manager;
        private readonly Discipline _discipline;
        private readonly Graph<Skill> _graph;

        public DisciplineEditMenu(IManager manager , Discipline discipline)
        {
            _manager = manager;
            _discipline = discipline;
            this._graph = manager.EditDiscipline(discipline);
        }

        public void RunMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("User name : admin\n" +
                                  "1)All skills.\n" +
                                  "2)Add skill\n" +
                                  "3)Delete skill.\n" +
                                  "4)Add ->.\n" +
                                  "5)Delete ->.\n" +
                                  "6)Back.\n" +
                                  "Enter the line number : ");
                if (!int.TryParse(Console.ReadLine(), out var num)) continue;
                var exitFlag = false;
                switch (num)
                {
                    case 1:
                    {
                        var vertex = PrintSkills();
                        if(vertex == null)
                            continue;
                        var newSkill = new SkillMenu(vertex).RunMenu();
                        vertex.Value = newSkill;
                        break;
                    }
                    case 2:
                        AddSkill();break;
                    case 3:
                        DelVertex();break;
                    case 4:
                        AddDependence();break;
                    case 5:
                        DelDependence();break;
                    case 6: exitFlag = true;break;
                }
                if (exitFlag)
                    break;
            }
            _manager.SaveDiscipline(_discipline, _graph);
        }

        private void AddDependence()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter begin skill's Id : ");
                if (int.TryParse(Console.ReadLine(), out var beginId))
                {
                    Console.WriteLine("Enter end skill's Id : ");
                    if (int.TryParse(Console.ReadLine(), out var endId))
                    {
                        try
                        {
                            var firstVertex = _graph.GetVertexById(beginId);
                            var secondVertex = _graph.GetVertexById(endId);
                            _graph.AddDependence(firstVertex, secondVertex);
                            Console.WriteLine("Press to continue.");
                            Console.ReadKey();
                            Console.CursorVisible = true;
                            break;
                        }
                        catch (KeyNotFoundException)
                        {
                            if (Check()) continue;
                            break;
                        }
                        catch (GraphException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Press to continue.");
                            Console.ReadKey();
                            if (Check()) continue;
                            break;
                        }
                    }
                    else
                    {
                        if (Check()) continue;
                        break;
                    }

                }
                else
                {
                    if (Check()) continue;
                    break;
                }
            }
        }

        private void DelDependence()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter begin skill's Id : ");
                if (int.TryParse(Console.ReadLine(), out var beginId))
                {
                    Console.WriteLine("Enter end skill's Id : ");
                    if (int.TryParse(Console.ReadLine(), out var endId))
                    {
                        try
                        {
                            var firstVertex = this._graph.GetVertexById(beginId);
                            var secondVertex = this._graph.GetVertexById(endId);
                            _graph.RemoveDependence(firstVertex, secondVertex);
                            Console.WriteLine("Press to continue.");
                            Console.ReadKey();
                            Console.CursorVisible = true;
                            break;
                        }
                        catch (KeyNotFoundException)
                        {
                            if (Check()) continue;
                            break;
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Press to continue.");
                            Console.ReadKey();
                            if (Check()) continue;
                            break;
                        }
                    }
                    else
                    {
                        if (Check()) continue;
                        break;
                    }

                }
                else
                {
                    if (Check()) continue;
                    break;
                }
            }

        }

        private void AddSkill()
        {
            Console.Clear();
            _graph.AddVertex(new Skill());
            Console.WriteLine($"skill's id = {_graph.MaxId - 1}");
            Console.WriteLine("Press to continue.");
            Console.ReadKey();
        }

        private void DelVertex()
        {
            Console.CursorVisible = true;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter vertex's Id : ");
                if (!int.TryParse(Console.ReadLine(), out var id))
                {
                    if (Check()) continue;
                    break;
                }
                try
                {
                    _graph.RemoveVertex(id);
                    Console.WriteLine("Press to continue.");
                    Console.ReadKey();
                    break;
                }
                catch (KeyNotFoundException)
                {
                    if (Check()) continue;
                    break;
                }
            }
        }

        private bool Check()
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Write 'R' if you want to repeat.");
            var flag = Console.ReadKey().Key == ConsoleKey.R;
            Console.Clear();
            return flag;

        }

        private Vertex<Skill> PrintSkills()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"total : {_graph.Count()}");
                foreach (var vertex in _graph)
                {
                    Console.WriteLine($"Skill id :{vertex.Id}");
                }
                Console.Write("Enter Skill id to get information (or exit if you want to exit) : ");
                var id = Console.ReadLine();
                if (string.Equals(id, "exit"))
                    return null;
                if (string.IsNullOrEmpty(id))
                    continue;
                if (int.TryParse(id, out var num))
                {
                    try
                    {
                        return _graph.GetVertexById(num);
                    }
                    catch ( KeyNotFoundException)
                    {
                        Console.WriteLine($"Skill with id = {id} not exist.\nPress to Continue.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine($"Skill with id = {id} not exist.\nPress to Continue.");
                    Console.ReadKey();
                }

            }
        }
    }
}
