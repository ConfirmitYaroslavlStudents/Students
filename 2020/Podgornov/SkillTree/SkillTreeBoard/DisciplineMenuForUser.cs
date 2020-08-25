using System;
using System.Collections.Generic;
using System.Linq;
using SkillTree;

namespace SkillTreeBoard
{
    class DisciplineMenuForUser
    {
        private readonly IUser _user;
        private readonly Discipline _discipline;
        private readonly GraphStatus<Skill> _graphStatus;

        public DisciplineMenuForUser(IUser user,Discipline discipline)
        {
            _user = user;
            _discipline = discipline;
            _graphStatus = user.LearnDiscipline(discipline);
        }
        public void RunMenu()
        {
            Console.Clear();
            var vertex = PrintSkills();
            if (vertex == null)
                return;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Skill id : {vertex.Id}\n" +
                                  $"1)About Skill\n" +
                                  $"2)Learn\n" +
                                  $"3)Dependencies.\n" +
                                  $"4)All Training time.\n" +
                                  $"5)Back\n"+
                                  $"Enter the line number : ");
                if (!int.TryParse(Console.ReadLine(), out var num)) continue;
                var exitFlag = false;
                switch (num)
                {
                    case 1:
                    {
                        Console.WriteLine(vertex.Value.ToString());
                        Console.ReadLine();
                        break;
                    }
                    case 2:
                    {
                        Console.Clear();
                        try
                        {
                            _graphStatus.FinishVertex(vertex);
                            Console.WriteLine($"Skill id :{vertex.Id} was learned.");
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        finally
                        {
                            Console.ReadKey();
                        }
                        break;
                    }
                    case 5:
                        exitFlag = true;break;
                    case 3:
                    {
                        Console.Clear();
                        foreach (var id in vertex.Dependencies)
                        {
                            Console.WriteLine($"Skill id : {id}");
                        }

                        Console.ReadKey();
                        break;
                    }
                    case 4:
                    {
                        Console.Clear();
                        Console.Write("All training time :");
                        Console.WriteLine(_graphStatus.Graph.GetDisciplineTanningTime(vertex));
                        Console.ReadKey();
                        break;
                    }
                }
                if (exitFlag)
                    break;
            }
            
            _user.SaveUsersDiscipline(_discipline, _graphStatus);
        }

        private Vertex<Skill> PrintSkills()
        {
            var graph = _graphStatus.Graph;
            while (true)
            {
                
                Console.Clear();
                Console.WriteLine($"total : {graph.Count()}");
                foreach (var vertex in graph)
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
                        return graph.GetVertexById(num);
                    }
                    catch (KeyNotFoundException)
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
