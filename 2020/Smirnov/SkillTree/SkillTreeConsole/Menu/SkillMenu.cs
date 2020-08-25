using System;
using SkillTree;
using System.Linq;

namespace SkillTreeConsole.Menu
{
    public class SkillMenu
    {
        private const string AddRequirementForSkill = "1";
        private const string GetAllInformationAboutSkill = "2";
        private const string GetAllWayUptoSkill = "3";
        private const string GetAllTimeUpToSkill = "4";
        private const string SaveAndExit = "5";
        private readonly Discipline ChooseDiscipline;
        
        public SkillMenu(Discipline chooseDiscipline)
        {
            ChooseDiscipline = chooseDiscipline;
        }

        private void WriteSkillMenu()
        {
            Console.WriteLine(ChooseDiscipline.Name);
            Console.WriteLine("1 Add requirement for skill");
            Console.WriteLine("2 Get all information about skill");
            Console.WriteLine("3 Get all way up to skill");
            Console.WriteLine("4 Get all time up to skill");
            Console.WriteLine("5 Save and return");
        }
        public bool WorkWithSkillMenu(DisciplineContainer disciplines)
        {
            //Console.Clear();
            WriteSkillMenu();
            var item = Console.ReadLine();
            var nameSkill = "";
            switch (item)
            {
                case (AddRequirementForSkill):
                    Console.Clear();
                    try
                    {
                        Console.WriteLine("Write name of ferst skill");
                        var firstSkill = ChooseDiscipline.Graph.TryGetVertex(Console.ReadLine()).Skill;
                        Console.WriteLine("Write name of second skill");
                        var secondSkill = ChooseDiscipline.Graph.TryGetVertex(Console.ReadLine()).Skill;

                    
                        disciplines.AddRequirementForSkill(ChooseDiscipline, firstSkill, secondSkill);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }

                    break;
                case (GetAllInformationAboutSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of skill");

                    try
                    {
                        var vertex = disciplines.GetSkill(ChooseDiscipline, Console.ReadLine());
                        Console.WriteLine($"Name = {vertex.Skill.Name} Difficult = {vertex.Skill.Difficult} " +
                                          $"Requirements =" + string.Join(" ", from edge in vertex.Edges
                                                                               select edge.ConnectedVertex.Skill.Name) +
                                          $"Specification = {vertex.Skill.Description} Time = {vertex.Skill.Time}");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    } 

                    break;
                case (GetAllWayUptoSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of skill");
                    nameSkill = Console.ReadLine();

                    try
                    {
                        Console.WriteLine(string.Join(" ", from skill in disciplines.GetAllWayUptoSkill(ChooseDiscipline, nameSkill).Reverse()
                                                           select skill.Name
                                                           ));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }

                    break;
                case (GetAllTimeUpToSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of skill");
                    nameSkill = Console.ReadLine();

                    try
                    {
                        Console.WriteLine(string.Join(" ", from skill in disciplines.GetAllWayUptoSkill(ChooseDiscipline, nameSkill).Reverse()
                                                           select $"{skill.Name} {skill.Time}"
                                                           ));
                        Console.WriteLine(disciplines.GetAllTimeUptoSkill(ChooseDiscipline, nameSkill));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }

                    break;
                case (SaveAndExit):
                    Loader.SkillTreeLoader.SaveDisciplines(disciplines.Disciplines);

                    return false;
                default:
                    Console.WriteLine("Incorrect Choice");
                    return false;
            }
            return true;
        }     
        
    }
}
