using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace SkillTree
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Classes.Discipline> disciplines = new List<Classes.Discipline>();

            XmlSerializer formatterDiscipline = new XmlSerializer(typeof(Classes.Discipline));
            Directory.CreateDirectory("Discipline");
            string[] files = Directory.GetFiles("Discipline");
            foreach (var file in files)
            {
                using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
                {
                    disciplines.Add((Classes.Discipline)formatterDiscipline.Deserialize(fs));

                }
            }
            Console.WriteLine("Select mode");
            Console.WriteLine("1 Admin");
            Console.WriteLine("2 User");
            string mode = Console.ReadLine();
            if (mode == "1")
            {
                var admin = new Classes.Admin("admin");
                do
                {
                    Console.WriteLine("1 Return all disciplines");
                    Console.WriteLine("2 Create new discipline");
                    Console.WriteLine("3 Add skill for discipline");
                    Console.WriteLine("4 Add requirement for skill");
                    Console.WriteLine("5 Return all scills for discipline");
                    Console.WriteLine("6 Return all information about skill");
                    Console.WriteLine("7 Save and Exit");
                    var nameDiscipline = "";
                    switch (Console.ReadLine())
                    {
                        case ("1"):
                            Console.Clear();
                            foreach (var discipline in disciplines)
                            {
                                Console.Write($"{discipline.Name} ");
                            }
                            Console.WriteLine();

                            break;
                        case ("2"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline");
                            admin.CreateNewDiscipline(Console.ReadLine(), disciplines);

                            break;
                        case ("3"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline");
                            nameDiscipline = Console.ReadLine();
                            Console.WriteLine("Write iformation about skill in format\"name difficult specification time\"");
                            string[] inputString = Console.ReadLine().Split();
                            admin.AddScillForDiscipline(nameDiscipline, new Classes.Skill(inputString[0], inputString[1],
                                inputString[2], int.Parse(inputString[3])), disciplines);

                            break;
                        case ("4"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline");
                            nameDiscipline = Console.ReadLine();
                            Console.WriteLine("Write name of ferst skill");
                            var firstSkill = Console.ReadLine();
                            Console.WriteLine("Write name of second skill");
                            var secondSkill = Console.ReadLine();
                            admin.AddRequirementForSkill(nameDiscipline, firstSkill, secondSkill, disciplines);

                            break;
                        case ("5"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline");
                            nameDiscipline = Console.ReadLine();
                            foreach (var discipline in disciplines)
                            {
                                if (discipline.Name == nameDiscipline)
                                {
                                    Console.WriteLine(discipline.ReturnNameAllSkills());
                                }
                            }

                            break;
                        case ("6"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline");
                            nameDiscipline = Console.ReadLine();
                            foreach (var discipline in disciplines)
                            {
                                if (discipline.Name == nameDiscipline)
                                {
                                    Console.WriteLine("Write name of skill");
                                    Console.WriteLine(discipline.FindSkill(Console.ReadLine()));
                                }
                            }

                            break;
                        case ("7"):
                            foreach (var discipline in disciplines)
                            {

                                using (FileStream fs = new FileStream(@"Discipline\" + discipline.Name + ".xml", FileMode.OpenOrCreate))
                                {
                                    formatterDiscipline.Serialize(fs, discipline);
                                }
                            }

                            return;
                    }
                }
                while (true);
            }
            else
            {
                var formatterPerson = new XmlSerializer(typeof(Classes.User));
                var user = new Classes.User();
                Console.WriteLine("1 Select user");
                Console.WriteLine("2 Create new user");
                if(Console.ReadLine() == "1")
                {
                    Console.WriteLine("Wrtie username");
                    var userName = Console.ReadLine();
                    Directory.CreateDirectory("Person");
                    using (var fs = new FileStream(@"Person\" + userName + ".xml", FileMode.OpenOrCreate))
                    {
                        user = (Classes.User)formatterDiscipline.Deserialize(fs);
                    }
                }
                else
                {
                    Console.WriteLine("Wrtie username");
                    user = new Classes.User(Console.ReadLine());
                }
                do
                {
                    Console.WriteLine("1 Return all disciplines");
                    Console.WriteLine("2 Return all available skills");
                    Console.WriteLine("4 Learn new skill");
                    Console.WriteLine("5 Learn new disciplines");
                    Console.WriteLine("6 Return all learned skills");
                    Console.WriteLine("7 Return all learned discipline");
                    Console.WriteLine("8 Return all scills for discipline");
                    Console.WriteLine("9 Return all information about skill");
                    Console.WriteLine("10 Save and Exit");
                    var nameDiscipline = "";
                    switch (Console.ReadLine())
                    {
                        case ("1"):
                            Console.Clear();
                            foreach (var discipline in disciplines)
                            {
                                Console.Write($"{discipline.Name} ");
                            }
                            Console.WriteLine();

                            break;
                        case ("2"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline");
                            nameDiscipline = Console.ReadLine();
                            foreach (var discipline in disciplines)
                            {
                                if (discipline.Name == nameDiscipline)
                                {
                                    Console.WriteLine("Write name of skill");
                                    var skillName = Console.ReadLine();
                                    var availableSkills = discipline.ReturnAvailableSkills(user.LearnedSkills);
                                    availableSkills = discipline.ReturnAvailableSkills(user.LearnedSkills);
                                    foreach (var skill in availableSkills)
                                    {
                                        if(skillName == skill.Name)
                                        {
                                            user.LearnNewSkill(skill);
                                        }
                                    }
                                }
                            }                           
                            Console.WriteLine();

                            break;
                        case ("3"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline ");
                            nameDiscipline = Console.ReadLine();
                            foreach (var discipline in disciplines)
                            {
                                if (discipline.Name == nameDiscipline)
                                {
                                    var availableSkills = discipline.ReturnAvailableSkills(user.LearnedSkills);
                                    availableSkills = discipline.ReturnAvailableSkills(user.LearnedSkills);
                                    foreach (var skill in availableSkills)
                                    {
                                        Console.WriteLine($"{skill} ");
                                    }
                                }
                            }
                            Console.WriteLine();

                            break;
                        case ("4"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline ");
                            nameDiscipline = Console.ReadLine();
                            foreach (var discipline in disciplines)
                            {
                                if (discipline.Name == nameDiscipline)
                                {
                                    user.LearnNewDiscipline(discipline);
                                }
                            }

                            break;
                        case ("5"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline");
                            nameDiscipline = Console.ReadLine();
                            foreach (var discipline in disciplines)
                            {
                                if (discipline.Name == nameDiscipline)
                                {
                                    Console.WriteLine(discipline.ReturnNameAllSkills());
                                }
                            }

                            break;
                        case ("6"):
                            Console.Clear();
                            Console.WriteLine(user.ReturnNameAllLearnedSkills());

                            break;
                        case ("7"):
                            Console.Clear();
                            Console.WriteLine(user.ReturnNameAllLearnedDisciplines());

                            break;
                        case ("8"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline");
                            nameDiscipline = Console.ReadLine();
                            foreach (var discipline in disciplines)
                            {
                                if (discipline.Name == nameDiscipline)
                                {
                                    Console.WriteLine("Write name of skill");
                                    Console.WriteLine(discipline.FindSkill(Console.ReadLine()));
                                }
                            }

                            break;
                        case ("9"):
                            Console.Clear();
                            Console.WriteLine("Write name of discipline ");
                            nameDiscipline = Console.ReadLine();
                            foreach (var discipline in disciplines)
                            {
                                if (discipline.Name == nameDiscipline)
                                {
                                    Console.WriteLine("Write name of skill");
                                    Console.WriteLine(discipline.FindSkill(Console.ReadLine()));
                                }
                            }

                            break;
                        case ("10"):
                            using (var fs = new FileStream(@"Person\" + user.Name + ".xml", FileMode.OpenOrCreate))
                            {
                                formatterPerson.Serialize(fs, user);
                            }
                            
                            return;

                    }
                }
                while (true);
            }

        }
    }
}            