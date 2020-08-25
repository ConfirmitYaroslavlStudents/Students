using System;
using SkillTree;
using System.Linq;
using SkillTree.Graph;


namespace SkillTreeRealization.Menu
{
    public class DisciplineMenu
    {
        private const string GetAllDisciplines = "1";
        private const string CreateNewDiscipline = "2";
        private const string AddRequirementForDiscipline = "3";
        private const string ReturnAllRequirementForDiscipline = "4";
        private const string SaveAndExit = "5";

        private static void WriteDisciplineMenu()
        {
            Console.WriteLine("1 Get all disciplines");
            Console.WriteLine("2 Create new discipline");
            Console.WriteLine("3 Add requirement for discipline");
            Console.WriteLine("4 Get all requirements for discipline");
            Console.WriteLine("5 Save and return");
        }
        public static bool WorkWithDisciplineMenu(DisciplineContainer disciplines)
        {
            Console.Clear();
            WriteDisciplineMenu();
            var item = Console.ReadLine();
            var chooseDiscipline = new Discipline();
            switch (item)
            {
                case (GetAllDisciplines):
                    Console.Clear();
                    Console.WriteLine(string.Join(" ", from discipline in disciplines.GetAllDisciplines() 
                                                       select discipline.Name));
                    Console.WriteLine();

                    break;
                case (CreateNewDiscipline):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    try
                    {
                        disciplines.CreateNewDiscipline(Console.ReadLine());
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }

                    break;
                case (AddRequirementForDiscipline):
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Write name of discipline");
                        chooseDiscipline = disciplines.GetDiscipline(Console.ReadLine());
                        Console.WriteLine("Write information about skill in format\"name difficult specification time\"");
                        string[] inputString = Console.ReadLine().Split();

                        disciplines.AddRequirementForDiscipline(chooseDiscipline,
                        new Skill(inputString[0], inputString[1],
                        inputString[2], int.Parse(inputString[3])));
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message == "Index was outside the bounds of the array." ? "information about skill not correct"
                                                                                                    : ex.Message);
                        Console.ReadKey();
                    }


                    break;
                case (ReturnAllRequirementForDiscipline):
                    Console.Clear();               
                    try
                    {
                        Console.WriteLine("Write name of discipline");
                        chooseDiscipline = disciplines.GetDiscipline(Console.ReadLine());
                    
                        Console.WriteLine(string.Join(" ", from skill in disciplines.GetAllSkillsForDiscipline(chooseDiscipline)
                                                           select skill.Name));
                    }
                    catch(InvalidOperationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message == "Sequence contains no elements" ? "Discipline not found" : ex.Message);
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
