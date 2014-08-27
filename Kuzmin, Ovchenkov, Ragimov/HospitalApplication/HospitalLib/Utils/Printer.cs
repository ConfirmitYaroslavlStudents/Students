using System;
using System.IO;
using HospitalLib.Interfaces;

namespace HospitalLib.Utils
{
    public class Printer : IPrint
    {
        private const string PathToFolder = @"\Printer\";

        private string GetFilePath(string name)
        {
            var printerPath = Environment.CurrentDirectory + PathToFolder + name;

            if (string.IsNullOrEmpty(printerPath))
                throw new InvalidDataException("Printer path is unknown");

            return printerPath  + ".txt";
        }

        public void Print(string name, string text)
        {
            using (var file = new StreamWriter(GetFilePath(name)))
            {
                file.Write(text);
            }
        }
    }
}
