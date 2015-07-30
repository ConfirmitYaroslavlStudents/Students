using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Mp3TagLib;

namespace mp3tager
{
    static public class Menu
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("Available commands:\n1. Load\n2. Changetags\n3. Rename\n4. Analysis\n5. Exit");
        }
        public static void ShowCurrentFile(IMp3File file)
        {
            if (file == null) return;
            Console.WriteLine("________________");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current file:");
            Console.ResetColor();
            Console.WriteLine(file.Name + ".mp3");
            Console.WriteLine("________________");
        }

        public static string GetUserChoice(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public static void ShowError(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("Press any key...\n\n");
            Console.ReadKey();
        }

        public static void SuccessMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successfully");
            Console.ResetColor();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        public static void ShowHelp()
        {
            Console.Clear();
            Console.Write("Available tags: ");
            foreach (var tag in Enum.GetValues(typeof(TagList)))
            {
                Console.Write(tag + ", ");
            }
            Console.WriteLine();
            Console.WriteLine("_______________________");
            Console.Write("          Example\n_______________________\nMask:\n");
            //[Touhou] ZUN - Faith is for the Transient People (Sanae's Theme) 2007
            Console.Write("[Touhou] ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{Artist}");
            Console.ResetColor();
            Console.Write(" - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{Title}");
            Console.ResetColor();
            Console.Write(" (Sanae's Theme) ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{Year}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("File name:");
            Console.Write("[Touhou] ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ZUN");
            Console.ResetColor();
            Console.Write(" - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Faith is for the Transient People");
            Console.ResetColor();
            Console.Write(" (Sanae's Theme) ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("2007");
            Console.ResetColor();
            Console.WriteLine("\n_______________________\n");
        }

        public static void ShowMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        public static void ShowInfo(string message)
        {
            Console.WriteLine(message);
        }

        public static void PrintCollection(string message,IEnumerable<string> collection)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
            Console.ResetColor();
        }
        public static void PrintTagValues(Dictionary<string, string> tagValues)
        {
            Console.Clear();
            foreach (var tagValue in tagValues)
            {
                Console.WriteLine("Tag: " + tagValue.Key + " value: " + tagValue.Value);
            }
        }
    }
}
