using System;
using System.Collections.Generic;
using SkillTree.Graph;
using SkillTreeContorller;

namespace SkillTreeRealization.Menu
{
    public class SkillMenu
    {
        private const string _returnAllDisciplines = "1";
        private const string _returnAllRequirementForDiscipline = "2";
        private const string _addRequirementForSkill = "3";
        private const string _returnAllInformationAboutSkill = "4";
        private const string _saveAndExit = "5";

        private static void WriteSkillMenu()
        {           
            Console.WriteLine("1 Return all disciplines");
            Console.WriteLine("2 Return all requirement for discipline");
            Console.WriteLine("3 Add requirement for skill");
            Console.WriteLine("4 Return all information about skill");
            Console.WriteLine("5 Save and Exit");
        }
        public static bool WorkWitchSkillMenu(Dictionary<string, Graph> disciplines)
        {
            WriteSkillMenu();
            var item = Console.ReadLine();
            var nameDiscipline = "";
            switch (item)
            {
                case (_returnAllDisciplines):
                    Console.Clear();
                    foreach (var discipline in disciplines)
                    {
                        Console.Write($"{discipline.Key} ");
                    }
                    Console.WriteLine();

                    break;
                case (_returnAllRequirementForDiscipline):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();

                    var NamesSkill = DisciplineController.ReturnNameAllScillsForDiscipline(nameDiscipline, disciplines);

                    foreach (var nameSkill in NamesSkill)
                    {
                        Console.WriteLine(nameSkill);
                    }

                    break;
                case (_addRequirementForSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write name of ferst skill");
                    var firstSkill = Console.ReadLine();
                    Console.WriteLine("Write name of second skill");
                    var secondSkill = Console.ReadLine();

                    SkillController.AddRequirementForSkill(nameDiscipline, firstSkill, secondSkill, disciplines);

                    break;
                case (_returnAllInformationAboutSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write name of skill"); 

                    Console.WriteLine(SkillController.ReturnAllInformationAboutSkill(nameDiscipline, Console.ReadLine(), disciplines));

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
