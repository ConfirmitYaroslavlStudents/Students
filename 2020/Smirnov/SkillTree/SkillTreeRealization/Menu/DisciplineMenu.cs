using System;
using System.Collections.Generic;
using SkillTree;
using SkillTree.Graph;
using SkillTreeRealization.SkillTree.Contorller;
using SkillTreeRealization.SkillTree.GetInfo;

namespace SkillTreeRealization.Menu
{
    public class DisciplineMenu
    {
        private const string _returnAllDisciplines = "1";
        private const string _createNewDiscipline = "2";
        private const string _addRequirementForDiscipline = "3";
        private const string _returnAllRequirementForDiscipline = "4";
        private const string _saveAndExit = "5";

        private static void WriteDisciplineMenu()
        {
            Console.WriteLine("1 Return all disciplines");
            Console.WriteLine("2 Create new discipline");
            Console.WriteLine("3 Add requirement for discipline");
            Console.WriteLine("4 Return all requirements for discipline");
            Console.WriteLine("5 Save and exit");
        }
        public static bool WorkWitchDisciplineMenu(List<Discipline> disciplines)
        {
            WriteDisciplineMenu();
            var item = Console.ReadLine();
            var nameDiscipline = "";
            switch (item)
            {
                case (_returnAllDisciplines):
                        Console.Clear();
                        foreach (var discipline in disciplines)
                        {
                            Console.Write($"{discipline.Name} ");
                        }
                        Console.WriteLine();

                    break;
                case (_createNewDiscipline):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    try
                    {
                        DisciplineController.CreateNewDiscipline(Console.ReadLine(), disciplines);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (_addRequirementForDiscipline):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write iformation about skill in format\"name difficult specification time\"");
                    string[] inputString = Console.ReadLine().Split();
                    try
                    {
                        DisciplineController.AddRequirementForDiscipline(nameDiscipline, new Skill(inputString[0], inputString[1],
                        inputString[2], int.Parse(inputString[3])), disciplines);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (_returnAllRequirementForDiscipline):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    try
                    {
                        Console.WriteLine(DisciplineGetInfo.ReturnNameAllSkillsForDiscipline(nameDiscipline, disciplines));
                    }
                    catch(InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (_saveAndExit):

                    Loader.SkillTreeLoader.SaveDisciplines(disciplines);

                    return false;
                default:
                    Console.WriteLine("Incorrect Choice");

                    return false;
            }
            return true;
        }
    }
}
