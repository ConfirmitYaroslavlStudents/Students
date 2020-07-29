using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SkillTree;

namespace SkillTreeBoard
{
    public class Program
    {
        public static void CreateAndRunMenu(ISkillTreeManager manager)
        {
            var names = new string[]{"Users Disciplines", "All Disciplines","Add Discipline" };
            var actions = new Action<string>[names.Length];
            actions[0] = (s =>
            {
                manager.IsUserDisciplinesOnFocus = true;
                CreateAndRunUsersDisciplinesMenu(manager, CreateAndRunUsersDisciplineMenu);
            });
            actions[1] = (s =>
            {
                manager.IsUserDisciplinesOnFocus = false;
                CreateAndRunUsersDisciplinesMenu(manager, CreateAndRunDisciplineMenu);
            });
            actions[2] = (s =>
            {
                manager.IsUserDisciplinesOnFocus = false;
                AddDiscipline(manager);
            });
           new ConsoleMenu<string[],string>(
                names,
                StringIndexator,
                actions
                ).Start();
        }

        private static void AddDiscipline(ISkillTreeManager manager)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter the name of the new discipline:");
                var name = Console.ReadLine();
                if (manager.Contains(name))
                {
                    Console.WriteLine("Discipline exists.");
                    if(Check()) continue;
                    break;
                }
                var graph = new Graph();
                manager.SaveGraph(name, graph);
                Console.WriteLine("Success");
                Console.WriteLine(KeyWords.ContinueString);
                Console.ReadKey();
                break;
            }
        }

        private static void CreateAndRunDisciplineMenu(ISkillTreeManager manager , string name)
        {
            var names = new string[] {"Delete", "Edit", "Download"};
            var actions = new Action<string>[names.Length];
            actions[0] = (s => Delete(manager, name));
            actions[1] = (s => CreateAndRunEditMenu(manager, name));
            actions[2] = (s => Download(manager, name));
            new ConsoleMenu<string[], string>(
                names,
                StringIndexator,
                actions
            ).Start();
        }

        private static void Download(ISkillTreeManager manager, string name)
        {
            Console.Clear();
            try
            {
                manager.DownloadGraph(name);
                Console.WriteLine("Success");
                Console.WriteLine(KeyWords.ContinueString);
                Console.ReadKey();
            }
            catch (FileNotFoundException e)
            {
                PrintExceptionMessage(e);
            }
        }

        private static void CreateAndRunEditMenu(ISkillTreeManager manager, string name)
        {
            try
            {
                var loadGraph = manager.LoadGraph(name);
                var names = new string[] {"All skills", "Save","Add skill","Delete skill","Add ->","Delete ->"};
                var actions = new Action<string>[names.Length];
                actions[0] = (s => { CreateAndRunAllSkillsMenu(loadGraph, CreateAndRunSkillMenu, GraphIndexator); });
                actions[1] = (s => { SaveGraph(manager, name, loadGraph); });
                actions[2] = (s => { AddSkill(loadGraph); });
                actions[3] = (s => { DelSkill(loadGraph); });
                actions[4] = (s => { AddDependence(loadGraph); });
                actions[5] = (s => { DelDependence(loadGraph); });
                new ConsoleMenu<string[], string>(
                    names,
                    StringIndexator,
                    actions
                ).Start();
            }
            catch (ArgumentNullException e)
            {
                PrintExceptionMessage(e);
            }
            catch (FileNotFoundException e)
            {
                PrintExceptionMessage(e);
            }
        }

        private static void AddDependence(Graph graph)
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
                            graph.AddDependence(beginId, endId);
                            Console.WriteLine(KeyWords.ContinueString);
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
                            PrintExceptionMessage(e);
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

        private static void DelDependence(Graph graph)
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
                            graph.RemoveDependence(beginId, endId);
                            Console.WriteLine(KeyWords.ContinueString);
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
                            PrintExceptionMessage(e);
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

        private static void AddSkill(Graph graph)
        {
            Console.Clear();
            graph.AddVertex(new Skill());
            Console.WriteLine($"skill's id = {graph.MaxId - 1}");
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void DelSkill(Graph graph)
        {
            Console.CursorVisible = true;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter skill's Id : ");
                if (!int.TryParse(Console.ReadLine(), out var id))
                {
                    if (Check()) continue;
                    break;
                }
                try
                {
                    graph.RemoveVertex(id);
                    Console.WriteLine(KeyWords.ContinueString);
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

        private static void CreateAndRunSkillMenu(Vertex vertex)
        {
            var names = new string[] {"About skill", "Edit Description", "Edit Training Time","Edit Complexity", "Dependencies" };
            var actions = new Action<string>[names.Length];
            actions[0] = (s => PrintSkillsInformation(vertex.Skill));
            actions[1] = (s => SetSkillDescription(vertex.Skill));
            actions[2] = (s => SetSkillTrainingTime(vertex.Skill));
            actions[3] = (s => SetSkillComplexity(vertex.Skill));
            actions[4] = (s => PrintDependenciesOfSkill(vertex));
            new ConsoleMenu<string[],string>(
                names,
                StringIndexator,
                actions
            ).Start();
        }

        private static void SetSkillComplexity(Skill skill)
        {
            Console.CursorVisible = true;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select the Complexity {0,1,2} :");
                var flag = int.TryParse(Console.ReadLine(), out var result);
                if (!flag || (result > 2 && result < 0))
                {
                    if(Check()) continue;
                    break;
                }
                skill.Complexity = (SkillComplexity)(result);
                break;
            }
        }

        private static void SetSkillDescription(Skill skill)
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Description:");
            skill.Description = Console.ReadLine();
        }

        private static void SetSkillTrainingTime(Skill skill)
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Training Time:");
            skill.TrainingTime = Console.ReadLine();
        }

        private static bool Check()
        {
            Console.Clear();
            Console.WriteLine("Invalid input. Write 'R' if you want to repeat.");
            var flag = Console.ReadKey().Key == ConsoleKey.R;
            Console.Clear();
            return flag;

        }

        private static void CreateAndRunUsersDisciplinesMenu(ISkillTreeManager manager, 
            Action<ISkillTreeManager,string> typeAction)
        {
            var names = manager.GetNamesOfAllGraphs().ToArray();
            var actions = new Action<string>[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                var name = names[i];
                actions[i] = (s => typeAction(manager, name));
            }

            new ConsoleMenu<string[], string>(
                names,
                StringIndexator,
                actions,
                (name => manager.Contains(name)?ConsoleColor.White:ConsoleColor.Red),
                $"RedFile - Deleted Files\n**********\ntotal:{names.Length}"
            ).Start();
        }

        private static void CreateAndRunUsersDisciplineMenu(ISkillTreeManager manager, string name)
        {
            var names = new string[] {"Delete", "Learn"};
            var actions = new Action<string>[names.Length];
            actions[0] = (s => Delete(manager, name));
            actions[1] = (s => CreateAndRunLearnMenu(manager, name));
            new ConsoleMenu<string[],string>(
                names,
                StringIndexator,
                actions
                ).Start();
        }

        private static void Delete(ISkillTreeManager manager, string name)
        {
            try
            {
                manager.DeleteGraph(name);
                Console.WriteLine("Success");
                Console.Write(KeyWords.ContinueString);
                Console.ReadKey();
            }
            catch (FileNotFoundException e)
            {
                PrintExceptionMessage(e);
            }
        }

        private static void CreateAndRunLearnMenu( ISkillTreeManager manager, string name)
        {
            try
            {
                var loadGraph = manager.LoadGraph(name);
                var names = new string[] {"All skills", "Save"};
                var actions = new Action<string>[2];
                actions[0] = (s => { CreateAndRunAllSkillsMenu(loadGraph, CreateAndRunVertexMenu, GraphUsersIndexator); });
                actions[1] = (s => { SaveGraph(manager, name, loadGraph); });
                new ConsoleMenu<string[], string>(
                    names,
                    StringIndexator,
                    actions
                ).Start();
            }
            catch (ArgumentNullException e)
            {
                PrintExceptionMessage(e);
            }
            catch (FileNotFoundException e)
            {
                PrintExceptionMessage(e);
            }
        }

        private static void PrintExceptionMessage(Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void SaveGraph(ISkillTreeManager manager, string name, Graph graph)
        {
            Console.Clear();
            manager.SaveGraph(name, graph);
            Console.WriteLine("Success.");
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void CreateAndRunAllSkillsMenu(Graph loadGraph , Action<Vertex> typeMenu ,
            MenuIndexator<Graph,Vertex> indexator)
        {
            var actions = new Action<Vertex>[loadGraph.MaxId];
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i] = typeMenu;
            }
            new ConsoleMenu<Graph, Vertex>(
                loadGraph,
                indexator,
                actions,
                GetVertexColor,
                KeyWords.DescriptionOfColors
            ).Start();
        }

        private static ConsoleColor GetVertexColor(Vertex vertex)
        {
            if (vertex.Finish)
            {
                return ConsoleColor.White;
            }
            return vertex.Available ? ConsoleColor.Green : ConsoleColor.Red;
        }

        private static void CreateAndRunVertexMenu(Vertex vertex)
        {
            var names = new string[]
            {
                "Dependencies", "About Skill", "Recognize"
            };
            var actions = new Action<string>[names.Length];
            actions[0] = (s => { PrintDependenciesOfSkill(vertex); });
            actions[1] = (s => { PrintSkillsInformation(vertex.Skill); });
            actions[2] = (s => { Recognize(vertex); });
            new ConsoleMenu<string[], string>(
                names,
                StringIndexator,
                actions
            ).Start();
        }

        private static void PrintSkillsInformation(Skill skill)
        {
            Console.Clear();
            Console.WriteLine(skill.ToString());
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void PrintDependenciesOfSkill(Vertex vertex)
        {
            Console.Clear();
            Console.WriteLine(KeyWords.DescriptionOfColors);
            var total = vertex.GetDependencies().Count;
            Console.WriteLine($"total: {total}");
            foreach (var currentVertex in vertex.GetDependencies())
            {
                PrintInColor(currentVertex);
            }
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void PrintInColor(Vertex vertex)
        {
            var consoleColor = GetVertexColor(vertex);
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(vertex.ToString());
            Console.ForegroundColor = temp;
        }

        private static void Recognize(Vertex vertex)
        {
            Console.Clear();
            try
            {
                vertex.Recognize();
                Console.WriteLine("Success.");
                Console.WriteLine(KeyWords.ContinueString);
                Console.ReadKey();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(KeyWords.ContinueString);
                Console.ReadKey();
            }
        }

        private static string StringIndexator(string[] item, int index) => item[index];

        private static Vertex GraphUsersIndexator(Graph graph, int index) => graph[index];

        private static Vertex GraphIndexator(Graph graph, int index) => graph.ElementAt(index);

    }
}
