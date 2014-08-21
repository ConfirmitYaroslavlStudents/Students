using System;
using System.Collections.Generic;
using System.IO;
using HospitalLib.Interfaces;

namespace HospitalLib.Database
{
    public class PersonProvider : IPersonsProvider
    {
        public static Person Person;

        public string DatabasePath { get; set; }
       
        public void Save(Person person)
        {
            var lines = GetLines(person);
            var path = GetFilePath(person);

            using (var file = new StreamWriter(path))
            {
                foreach (var line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }

        public IEnumerable<Person> Load()
        {
            const string fileNameTemplate = "*";
            var result = new List<Person>();

            var templatePath = Directory.GetFiles(DatabasePath, fileNameTemplate);

            if (templatePath.Length == 0)
            {
                return null;
            }

            foreach (var path in templatePath)
            {
                result.Add(LoadPersonFromFile(path));
            }

            return result;
        }

        private string GetFilePath(Person person)
        {
            if (string.IsNullOrEmpty(DatabasePath))
                throw new InvalidDataException("Database path is unknown");

            return String.Format("{0}{1}{2}{3}{4}{5}", DatabasePath,"\\", person.SecondName, "_", person.FirstName, ".txt");
        }

        private Person LoadPersonFromFile(string fileName)
        {
            var personInformation = new List<string>();
            using (var file = new StreamReader(fileName))
            {
                string line;
                while((line = file.ReadLine()) != null)
                {
                    personInformation.Add(line);
                }
            }
            var firstName = GetAttribute("Имя : ", personInformation[0]);
            var secondName = GetAttribute("Фамилия : ", personInformation[1]);
            var age = GetAttribute("Возраст : ", personInformation[2]);
            var adress = GetAttribute("Адрес : ", personInformation[3]);
            var note = GetAttribute("Примечание : ", personInformation[4]);
          
            var person = new Person
            {
                FirstName = firstName,
                SecondName = secondName,
                Age = int.Parse(age),
                Adress = adress,
                Note = note
            };

            return person;
        }

        private string GetAttribute(string garbage, string personInformation)
        {
            return personInformation.Replace(garbage, "");
        }

        private IEnumerable<string> GetLines(Person person)
        {
            var result = new List<string>
            {
                string.Format("Имя : {0}", person.FirstName),
                string.Format("Фамилия : {0}", person.SecondName),
                string.Format("Возраст : {0}", person.Age),
                string.Format("Адрес : {0}", person.Adress),
                string.Format("Примечание : {0}", person.Note)
            };

            return result;
        }
    }
}
