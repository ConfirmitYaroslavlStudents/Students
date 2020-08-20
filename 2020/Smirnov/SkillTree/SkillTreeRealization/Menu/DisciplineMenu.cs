using System;
using System.Collections.Generic;
using SkillTree;
using SkillTree.Graph;
using SkillTreeRealization.SkillTree;


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
        public static bool WorkWitchDisciplineMenu(DisciplineContainer disciplines)
        {
            WriteDisciplineMenu();
            var item = Console.ReadLine();
            var nameDiscipline = "";
            switch (item)
            {
                case (GetAllDisciplines):
                    Console.Clear();
                    Console.WriteLine(disciplines.GetAllDisciplines());
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
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (AddRequirementForDiscipline):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write iformation about skill in format\"name difficult specification time\"");
                    string[] inputString = Console.ReadLine().Split();
                    try
                    {
                        disciplines.AddRequirementForDiscipline(nameDiscipline, new Skill(inputString[0], inputString[1],
                        inputString[2], int.Parse(inputString[3])));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    break;
                case (ReturnAllRequirementForDiscipline):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    try
                    {
                        Console.WriteLine(disciplines.GetNameAllSkillsForDiscipline(nameDiscipline));
                    }
                    catch(InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
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
