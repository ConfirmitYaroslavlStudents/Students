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

            List<Classes.Discipline> Discipline = new List<Classes.Discipline>();
            do
            {
                Console.WriteLine("1 Create new discipline");
                Console.WriteLine("2 Return all scills for discipline");
                Console.WriteLine("3 Save and Exit");
                switch (Console.ReadLine())
                {
                    case ("1"):
                        Console.Clear();
                        Classes.Admin admin = new Classes.Admin("admin");

                        admin.CreateNewDiscipline("Programming", ref Discipline);

                        List<Classes.Skill> skills = new List<Classes.Skill>();
                        skills.Add(new Classes.Skill("Encapsulation", "easy", "important", new List<Classes.Skill>(), 10));
                        skills.Add(new Classes.Skill("Polymorphism", "easy", "important", new List<Classes.Skill>(), 15));
                        skills.Add(new Classes.Skill("Inheritance", "easy", "important", new List<Classes.Skill>(), 20));

                        admin.CreateNewDiscipline("OOP", skills, ref Discipline);
                                               
                        break;
                    case ("2"):
                        Console.Clear();
                        Console.WriteLine("Write name discipline ");
                        string Name = Console.ReadLine();
                        foreach(var c in Discipline)
                        {
                            if (c.Name == Name)
                                Console.WriteLine(c.ReturnNameAllSkills());
                        }
                        break;
                    case ("3"):
                        foreach (var c in Discipline)
                        {
                            XmlSerializer formatter = new XmlSerializer(typeof(Classes.Discipline));
                            using (FileStream fs = new FileStream(@"Discipline\" + c.Name + ".xml", FileMode.OpenOrCreate))
                            {
                                formatter.Serialize(fs, c);
                            }
                        }
                        return;                       
                }                
            }
            while (true);

        }
    }
}
