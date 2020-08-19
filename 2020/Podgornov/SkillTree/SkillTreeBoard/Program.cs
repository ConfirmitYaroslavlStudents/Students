using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SkillTree;

namespace SkillTreeBoard
{
    public class Program
    {
        public static void RunMenu(ISkillTreeManager manager)
        {
            var names = new [] {"User", "Moderator"};
            var actions = new Action<string>[names.Length];
            var specialAction = new Action<Vertex<Discipline>>((discipline => { RunModeratorDisciplineMenu(manager, discipline.Value); }));
            actions[0] = s => { RunUserMenu(manager);};
            actions[1] = s => { RunModeratorMenu(manager, specialAction); }; 
            new ConsoleMenu<string[], string>(
                names,
                actions
            ).Start();
        }

        private static void RunUserMenu(ISkillTreeManager manager)
        {
            var names = new [] {"User's Disciplines","Del User's Discipline", "Download Disciplines"};
            var actions = new Action<string>[names.Length];
            actions[0] = s =>
            {
                manager.IsUserDisciplinesOnFocus = true;
                RunUsersDisciplineMenu(manager);
            };
            actions[1] = s => { Delete(manager); };  
            actions[2] = s =>
            {
                manager.IsUserDisciplinesOnFocus = false;
                var specialAction = new Action<Vertex<Discipline>>((discipline =>
                {
                    RunUserAllDisciplineMenu(manager, discipline.Value);
                }));
                RunAllDisciplineMenu(manager, specialAction);
            };
            new ConsoleMenu<string[], string>(
                names,
                actions
            ).Start();
        }

        private static void RunUsersDisciplineMenu(ISkillTreeManager manager)
        {
            var disciplines = manager.ToArray();//;
            var actions = new Action<string>[disciplines.Count()];
            for (int i = 0; i < actions.Length; i++)
            {
                var i1 = i;
                actions[i] = s =>
                {
                    RunUsersDisciplineMenu(manager,disciplines[i1]);
                };
            }

            new ConsoleMenu<IEnumerable<string>,string>(
                (from dis in manager select $"Element {dis.Id}"),
                actions
                ).Start();
        }

        private static void RunUserAllDisciplineMenu(ISkillTreeManager manager,Discipline discipline)
        {
            var names = new [] {"Download", "About Discipline", "Dependencies"};
            var actions = new Action<string>[names.Length];
            actions[0] = s => Download(manager, discipline);
            actions[1] = s => {PrintVertexInformation(discipline); };
            actions[2] = s =>
            {
                PrintDependenciesOfVertex(manager.DisciplinesGraph[discipline.Id]);
            };
            new ConsoleMenu<string[],string>(
                names,
                actions
            ).Start();
        }

        private static void Download(ISkillTreeManager manager, Discipline discipline)
        {
            Console.Clear();
            try
            {
                if (!manager.DisciplinesGraph[discipline.Id].Available)
                    throw new InvalidOperationException("Discipline not available.");
                manager.DownloadGraph(discipline);
                Console.WriteLine("Success");
                Console.WriteLine(KeyWords.ContinueString);
                Console.ReadKey();
            }
            catch (FileNotFoundException e)
            {
                PrintExceptionMessage(e);
            }
            catch (InvalidOperationException e)
            {
                PrintExceptionMessage(e);
            }
        }

        private static void RunUsersDisciplineMenu(ISkillTreeManager manager, Discipline discipline)
        {
            var names = new [] { "About Discipline", "Learn" };
            var actions = new Action<string>[names.Length];
            actions[0] = (s => PrintVertexInformation(discipline));
            actions[1] = (s => CreateAndRunLearnMenu(manager, discipline.Id.ToString()));
            new ConsoleMenu<string[], string>(
                names,
                actions
            ).Start();
        }

        private static void CreateAndRunLearnMenu(ISkillTreeManager manager, string name)
        {
            try
            {
                var loadGraph = manager.LoadGraph<Skill>(name);
                var names = new [] { "All skills", "Save" };
                var actions = new Action<string>[2];
                actions[0] = (s => { CreateAndRunAllVertexMenu(loadGraph, CreateAndRunSkillMenu); });
                actions[1] = (s => { SaveGraph(manager, name, loadGraph); });
                new ConsoleMenu<string[], string>(
                    names,
                    actions
                ).Start();
                if (loadGraph.All((vertex => vertex.Finished)))
                {
                    try
                    {
                        manager.DisciplinesGraph[int.Parse(name)].Finish();
                    }
                    catch (KeyNotFoundException)
                    {
                        //Changes don't save because discipline was deleted.
                    }
                }
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

        private static readonly string[] DisciplineConstructorNames = new[]
        {
            "All Disciplines","Add Discipline", "Del Discipline", "add ->","del ->"
        };

        private static void RunModeratorMenu(ISkillTreeManager manager , Action<Vertex<Discipline>> specialAction)
        {
            manager.IsUserDisciplinesOnFocus = false;
            var names = DisciplineConstructorNames;
            var actions = new Action<string>[names.Length];
            actions[0] = (s => { RunAllDisciplineMenu(manager, specialAction); });
            actions[1] = (s => { AddDiscipline(manager); });
            actions[2] = (s => { Delete(manager); });
            actions[3] = (s => { AddDependence(manager.DisciplinesGraph); });
            actions[4] = (s => { DelDependence(manager.DisciplinesGraph); });
            new ConsoleMenu<string[], string>(
                names,
                actions
            ).Start();
            manager.SaveCondition();
        }

        private static void RunAllDisciplineMenu(ISkillTreeManager manager , Action<Vertex<Discipline>> specialAction)
        {
            var actions = new Action<Vertex<Discipline>>[manager.Count()];
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i] = specialAction;
            }
            new ConsoleMenu<Graph<Discipline>, Vertex<Discipline>>(
                manager.DisciplinesGraph,
                actions,
                GetVertexColor,
                KeyWords.DescriptionOfColors,
                true
            ).Start();
        }

        private static void AddDiscipline(ISkillTreeManager manager)
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the new discipline:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the description of the new discipline:");
            var description = Console.ReadLine();
            var id = manager.DisciplinesGraph.MaxId;
            manager.DisciplinesGraph.AddVertex(new Discipline(id, name, description));
            var graph = new Graph<Skill>();
            manager.SaveGraph(id.ToString(), graph);
            Console.WriteLine("Success");
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void Delete(ISkillTreeManager manager)
        {
            Console.CursorVisible = true;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter Discipline's Id : ");
                if (!int.TryParse(Console.ReadLine(), out var id))
                {
                    if (Check()) continue;
                    break;
                }
                try
                {
                    manager.DeleteGraph(id.ToString());
                    Console.WriteLine("Success");
                    Console.WriteLine(KeyWords.ContinueString);
                    Console.ReadKey();
                    break;
                }
                catch (KeyNotFoundException)
                {
                    if (Check()) continue;
                    break;
                }
                catch (FileNotFoundException e)
                {
                    PrintExceptionMessage(e);
                    break;
                }
            }
        }

        private static void RunModeratorDisciplineMenu(ISkillTreeManager manager , Discipline discipline)
        {
            var names = new [] {"Edit Discipline" ,"About" ,"Edit Name","Edit Description"};
            var actions = new Action<string>[names.Length];
            actions[0] = (s => RunModeratorEditMenu(manager, discipline));
            actions[1] = (s => { PrintVertexInformation(discipline); });
            actions[2] = (s => { SetDisciplineName(discipline); });
            actions[3] = (s => { SetDisciplineDescription(discipline); });
            new ConsoleMenu<string[], string>(
                names,
                actions
            ).Start();
        }

        private static void SetDisciplineName(Discipline discipline)
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the discipline:");
            var name = Console.ReadLine();
            discipline.Name = name;
            Console.WriteLine("Success");
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void SetDisciplineDescription(Discipline discipline)
        {
            Console.Clear();
            Console.WriteLine("Enter the description of the discipline:");
            var description = Console.ReadLine();
            discipline.Description = description;
            Console.WriteLine("Success");
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static readonly string[] SkillConstructorNames = new string[]
        {
            "All skills", "Save", "Add skill", "Delete skill", "Add ->", "Delete ->"
        };

        private static void RunModeratorEditMenu(ISkillTreeManager manager, Discipline discipline)
        {
            try
            {
                var loadGraph = manager.LoadGraph<Skill>(discipline.Id.ToString());
                var names = SkillConstructorNames;
                var actions = new Action<string>[names.Length];
                actions[0] = (s => { CreateAndRunAllVertexMenu(loadGraph, RunSkillMenu); });
                actions[1] = (s => { SaveGraph(manager, discipline.Id.ToString(), loadGraph); });
                actions[2] = (s => { AddSkill(loadGraph); });
                actions[3] = (s => { DelVertex(loadGraph); });
                actions[4] = (s => { AddDependence(loadGraph); });
                actions[5] = (s => { DelDependence(loadGraph); });
                new ConsoleMenu<string[], string>(
                    names,
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

        private static void AddDependence<T>(Graph<T> graph)
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

        private static void DelDependence<T>(Graph<T> graph)
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

        private static void AddSkill(Graph<Skill> graph)
        {
            Console.Clear();
            graph.AddVertex(new Skill());
            Console.WriteLine($"skill's id = {graph.MaxId - 1}");
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void DelVertex(Graph<Skill> graph)
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

        private static void RunSkillMenu(Vertex<Skill> vertex)
        {
            var names = new [] { "About skill", "Edit Description", "Edit Training Time", "Edit Complexity", "Dependencies" };
            var actions = new Action<string>[names.Length];
            actions[0] = (s => PrintVertexInformation(vertex.Value));
            actions[1] = (s => SetSkillDescription(vertex.Value));
            actions[2] = (s => SetSkillTrainingTime(vertex.Value));
            actions[3] = (s => SetSkillComplexity(vertex.Value));
            actions[4] = (s => PrintDependenciesOfVertex(vertex));
            new ConsoleMenu<string[], string>(
                names,
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
                    if (Check()) continue;
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

                skill.TrainingTime = (result);
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

        private static void PrintExceptionMessage(Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void SaveGraph<T>(ISkillTreeManager manager, string name, Graph<T> graph)
        {
            Console.Clear();
            manager.SaveGraph(name, graph);
            Console.WriteLine("Success.");
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void CreateAndRunAllVertexMenu<T>(Graph<T> loadGraph, Action<Vertex<T>> typeMenu)
        {
            var actions = new Action<Vertex<T>>[loadGraph.Count()];
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i] = typeMenu;
            }
            new ConsoleMenu<Graph<T>, Vertex<T>>(
                loadGraph,
                actions,
                GetVertexColor,
                KeyWords.DescriptionOfColors,
                true
            ).Start();
        }

        private static ConsoleColor GetVertexColor<T>(Vertex<T> vertex)
        {
            if (vertex.Finished)
            {
                return ConsoleColor.White;
            }
            return vertex.Available ? ConsoleColor.Green : ConsoleColor.Red;
        }

        private static void CreateAndRunSkillMenu(Vertex<Skill> vertex)
        {
            var names = new []
            {
                "Dependencies", "About Value", "Finish","The time to study the branch."
            };
            var actions = new Action<string>[names.Length];
            actions[0] = (s => { PrintDependenciesOfVertex(vertex); });
            actions[1] = (s => { PrintVertexInformation(vertex.Value); });
            actions[2] = (s => { Recognize(vertex); });
            actions[3] = s => { GetTimeToStudyTheBranch(vertex);};
            new ConsoleMenu<string[], string>(
                names,
                actions
            ).Start();
        }

        private static void GetTimeToStudyTheBranch(Vertex<Skill> vertex )
        {
            Console.Clear();
            var time = vertex.GetAllDependencies().Select(i => i.Value.TrainingTime).Sum();
            Console.WriteLine($"The time to study the branch : {time}");
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void PrintVertexInformation<T>(T value)
        {
            Console.Clear();
            Console.WriteLine(value.ToString());
            Console.WriteLine(KeyWords.ContinueString);
            Console.ReadKey();
        }

        private static void PrintDependenciesOfVertex<T>(Vertex<T> vertex)
        {
            new ConsoleMenu<IEnumerable<Vertex<T>>,Vertex<T>>(
                vertex.GetAllDependencies(),
                null,
                GetVertexColor,
                KeyWords.DescriptionOfColors,
                true
                ).Start();
        }

        private static void Recognize<T>(Vertex<T> vertex)
        {
            Console.Clear();
            try
            {
                vertex.Finish();
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

    }
}
