using System;
using System.Collections.Generic;
using Mp3TagLib;

namespace mp3tager
{
    static public class Menu
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("Available commands:\n1. Changetags\n2. Rename\n3. Analysis\n4. Sync\n5. Exit");
        }

        public static void PrintCurrentFile(IMp3File file)
        {
            if (file == null) return;
            Console.WriteLine("________________");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current file:");
            Console.ResetColor();
            Console.WriteLine(file.Name + ".mp3");
            Console.WriteLine("________________");
        }

        public static string GetUserInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public static void PrintError(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("Press any key...\n\n");
            Console.ReadKey();
        }

        public static void PrintSuccessMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successfully");
            Console.ResetColor();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        public static void PrintHelp()
        {
            Console.Clear();
            Console.Write("Available tags: ");
            foreach (var tag in Enum.GetValues(typeof(Tags)))
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

        public static void PrintMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void PrintCollection(string message, IEnumerable<string> collection, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
            Console.ResetColor();
        }

        public static void PrintCollection(string message, Dictionary<string,string> collection,ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            foreach (var item in collection)
            {
                Console.WriteLine("______________________________");
                Console.WriteLine(item.Key);
                Console.ForegroundColor = color;
                Console.WriteLine(item.Value);
                Console.ResetColor();
                Console.WriteLine("______________________________");
            }
        }

        public static void PrintTagValues(Dictionary<string, string> tagValues)
        {
            Console.Clear();
            foreach (var tagValue in tagValues)
            {
                Console.WriteLine("Tag: " + tagValue.Key + " value: " + tagValue.Value);
            }
        }

        public static void PrintChanges(IEnumerable<IMp3File> files)
        {
            foreach (var file in files)
            {
                var mp3File = file as Mp3File;
                if (mp3File.NameChanged)
                {
                    Console.WriteLine("_____________________________");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0}.mp3", mp3File.Name);
                    Console.ResetColor();
                    Console.Write(" renamed to ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0}.mp3 ", mp3File.NewName);
                    Console.ResetColor();
                    Console.WriteLine("_____________________________");
                }
                if (mp3File.TagChanged)
                {
                    var tags = mp3File.GetTags();
                    Console.WriteLine("_____________________________");
                    Console.Write("Tags changed in ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0}.mp3",mp3File.Name);
                    Console.ResetColor();
                    if (tags.Album!=mp3File.OldTags.Album)
                        Console.WriteLine("{0} [{1}]->[{2}]", Tags.Album, mp3File.OldTags.Album, tags.Album);
                    if (tags.Artist != mp3File.OldTags.Artist)
                        Console.WriteLine("{0} [{1}]->[{2}]", Tags.Artist, mp3File.OldTags.Artist, tags.Artist);
                    if (tags.Comment!= mp3File.OldTags.Comment)
                        Console.WriteLine("{0} [{1}]->[{2}]", Tags.Comment, mp3File.OldTags.Comment, tags.Comment);
                    if (tags.Genre != mp3File.OldTags.Genre)
                        Console.WriteLine("{0} [{1}]->[{2}]", Tags.Genre, mp3File.OldTags.Genre, tags.Genre);
                    if (tags.Title != mp3File.OldTags.Title)
                        Console.WriteLine("{0} [{1}]->[{2}]", Tags.Title, mp3File.OldTags.Title, tags.Title);
                    if (tags.Track != mp3File.OldTags.Track)
                        Console.WriteLine("{0} [{1}]->[{2}]", Tags.Track, mp3File.OldTags.Track, tags.Track);
                    if (tags.Year != mp3File.OldTags.Year)
                        Console.WriteLine("{0} [{1}]->[{2}]", Tags.Year, mp3File.OldTags.Year, tags.Year);
                    Console.WriteLine("_____________________________");
                }
            }
        }
    }
}
