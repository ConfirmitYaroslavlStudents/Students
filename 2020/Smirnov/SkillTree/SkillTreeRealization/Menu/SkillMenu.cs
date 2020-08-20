using System;
using System.Collections.Generic;
using SkillTree;
using SkillTreeRealization.SkillTree;
using System.Linq;

namespace SkillTreeRealization.Menu
{
    public class SkillMenu
    {
        private const string AddRequirementForSkill = "1";
        private const string GetAllInformationAboutSkill = "2";
        private const string GetAllWayUptoSkill = "3";
        private const string GetAllTimeUpToSkill = "4";
        private const string SaveAndExit = "5";

        private static void WriteSkillMenu()
        {           
            Console.WriteLine("1 Add requirement for skill");
            Console.WriteLine("2 Get all information about skill");
            Console.WriteLine("3 Get all way up to skill");
            Console.WriteLine("4 Get all time up to skill");
            Console.WriteLine("5 Save and return");
        }
        public static bool WorkWitchSkillMenu(DisciplineContainer disciplines)
        {
            WriteSkillMenu();
            var item = Console.ReadLine();
            var nameDiscipline = "";
            var nameSkill = "";
            switch (item)
            {
                case (AddRequirementForSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write name of ferst skill");
                    var firstSkill = Console.ReadLine();
                    Console.WriteLine("Write name of second skill");
                    var secondSkill = Console.ReadLine();

                    try
                    {
                        disciplines.AddRequirementForSkill(nameDiscipline, firstSkill, secondSkill);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (GetAllInformationAboutSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write name of skill");

                    try
                    {
                        Console.WriteLine(disciplines.GetAllInformationAboutSkill(nameDiscipline, Console.ReadLine()));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    } 

                    break;
                case (GetAllWayUptoSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write name of skill");
                    nameSkill = Console.ReadLine();

                    try
                    {
                        Console.WriteLine(disciplines.GetAllWayUptoSkill(nameDiscipline, nameSkill));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (GetAllTimeUpToSkill):
                    Console.Clear();
                    Console.WriteLine("Write name of discipline");
                    nameDiscipline = Console.ReadLine();
                    Console.WriteLine("Write name of skill");
                    nameSkill = Console.ReadLine();

                    try
                    {
                        Console.WriteLine(disciplines.GetAllTimeUptoSkill(nameDiscipline, nameSkill));
                    }
                    catch (InvalidOperationException ex)
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
