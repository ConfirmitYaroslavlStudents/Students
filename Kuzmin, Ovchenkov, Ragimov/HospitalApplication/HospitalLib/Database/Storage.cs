using System;
using System.Collections.Generic;
using System.IO;
using HospitalLib.Interfaces;

namespace HospitalLib.Database
{
    public class Storage : IStorage
    {
        public string DatabasePath { get; set; }

        public IList<Template.Template> Search(Person person, ITemplateProvider templateProvider)
        {
            var result = new List<Template.Template>();
            var fileNameTemplate = GetFileNameTemplate(person);
            DatabasePath = DatabasePath + "\\";
            var templatePath = Directory.GetFiles(DatabasePath, fileNameTemplate);

            if (templatePath.Length == 0)
            {
                return null;
            }

            foreach (var path in templatePath)
            {
                result.Add(templateProvider.Load(path));
            }

            return result;
        }
       
        public void Save(Template.Template template, Person person)
        {
            var path = GetFilePath(person, template);

            using (var file = new StreamWriter(path))
            {
               file.Write(template.ToString());
            }
        }

        private string GetFileNameTemplate(Person person)
        {
            return String.Format("{0}{1}{2}{3}", person.SecondName, "_", person.FirstName, "*");
        }

        private string GetFilePath(Person person, Template.Template template)
        {
            if (string.IsNullOrEmpty(DatabasePath))
            {
                throw new InvalidDataException("Database path is unknown");
            }

            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", DatabasePath,"\\", person.SecondName, "_", person.FirstName, "_",template.TemplateType, ".txt");
        }
    }
}
