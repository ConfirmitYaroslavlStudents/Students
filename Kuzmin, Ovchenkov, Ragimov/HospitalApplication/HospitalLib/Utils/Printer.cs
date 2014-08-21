using System;
using System.Collections.Generic;
using System.IO;
using HospitalLib.Database;
using HospitalLib.Interfaces;

namespace HospitalLib.Utils
{
    public class Printer : IPrint
    {
        public string PrinterPath { get; set; }

        public void Print(Template.Template template, Person person)
        {
            using (var file = new StreamWriter(GetFilePath(person)))
            {
               file.Write(template.ToString());
            }
        }

        private string GetFilePath(Person person)
        {
            if (string.IsNullOrEmpty(PrinterPath))
                throw new InvalidDataException("Database path is unknown");

            return String.Format("{0}{1}{2}{3}{4}{5}", PrinterPath, "\\", person.SecondName, "_", person.FirstName, ".txt");
        }

        private IEnumerable<string> GetLines(Template.Template template)
        {
            var result = new List<string>();

            foreach (var line in template.Lines)
            {
                result.Add(line.ToString());
            }

            return result;
        }
    }
}
