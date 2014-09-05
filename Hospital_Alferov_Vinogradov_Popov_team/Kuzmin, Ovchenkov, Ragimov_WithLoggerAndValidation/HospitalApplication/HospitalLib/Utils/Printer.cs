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
            catch
            {
                MessageBox.Show("Не удалось распечатать файл!", "Error");
            }
        }

        private string GetFilePath(string name)
        {
            string printerPath = Environment.CurrentDirectory + PathToFolder + name;

            if (string.IsNullOrEmpty(printerPath))
                throw new InvalidDataException("Printer path is unknown");

            return printerPath + ".txt";
        }
    }
}