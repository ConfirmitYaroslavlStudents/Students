using System;
using System.IO;
using System.Windows;
using HospitalLib.Interfaces;

namespace HospitalLib.Utils
{
    public class Printer : IPrint
    {
        private const string PathToFolder = @"\Printer\";

        public void Print(string name, string text)
        {
            try
            {
                using (var file = new StreamWriter(GetFilePath(name)))
                {
                    file.Write(text);
                }
            }
            catch (IOException e)
            {
                throw new IOException("Не удалось распечатать файл! " + e.Message);
            }   
        }

        private string GetFilePath(string name)
        {
            var printerPath = Environment.CurrentDirectory + PathToFolder + name;

            if (string.IsNullOrEmpty(printerPath))
                throw new InvalidDataException("Путь к принтеру не указан!");

            return printerPath  + ".txt";
        }
    }
}
