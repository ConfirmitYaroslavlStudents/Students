using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mp3TagLib;


namespace mp3tager
{
    class Program
    {
        static void Main()
        {
            var tager=new Tager(new FileLoader());
            while (true)
            {
                Console.Clear();
                ShowMenu();
                ShowCurrentFile(tager.CurrentFile);
                Console.Write("\n\nCommand:");
                switch (Console.ReadLine().ToLower())
                {
                    case "load":
                        Console.Clear();
                        if (!tager.Load(GetPath()))
                        {
                            Console.Clear();
                            ShowError("File does not exist");
                            Console.ReadKey();
                        }
                break;
                    case "save":
                        Console.Clear();
                        try
                        {
                            tager.Save();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Successfully");
                            Console.ResetColor();
                            Console.WriteLine("Press any key...");
                        }
                        catch (Exception e)
                        {

                            Console.Clear();
                            ShowError(e.Message);
                        }
                        Console.ReadKey();
                        break;
                    case "changetags":
                        try
                        {
                            Console.Clear();
                            ShowExample();
                            ShowCurrentFile(tager.CurrentFile);
                            tager.ChangeTags(GetTagsFromFileName(tager.CurrentFile));
         
                        }
                        catch (Exception e)
                        {
                            Console.Clear();
                            ShowError(e.Message);
                            Console.ReadKey();
                        }
                        break;
                    case "rename":
                        if (tager.CurrentFile == null)
                        {
                            Console.Clear();
                            ShowError("File is not loaded");
                            Console.ReadKey();
                            break;
                        }
                        try
                        {
                            Console.Clear();
                            tager.ChangeName(new Mask(GetMask()));
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Successfully");
                            Console.ResetColor();
                            Console.WriteLine("Press any key...");
                        }
                        catch (Exception e)
                        {
                            Console.Clear();
                            ShowError(e.Message);
                        }
                        Console.ReadKey();
                        break;
                    case "analysis":
                        try
                        {
                            Console.Clear();
                            AnalyzeFolder(@"C:\Users\Alexandr\Desktop\TEST");
                        }
                        catch (Exception e)
                        {

                            Console.Clear();
                            ShowError(e.Message);
                        }
                        break;
                    case "exit":
                        return;
                    default:
                        Console.Clear();
                        ShowError("Error comand");
                        Console.ReadKey();
                        break;

                }

            }


        }

        public static void AnalyzeFolder(string path)
        {
            var files = Directory.GetFiles(path);
            var tager=new Tager(new FileLoader());
            var mask=new Mask(GetMask());
            var badFiles=new List<string>();
            foreach (var file in files.Where(file => file.EndsWith(".mp3")))
            {
                tager.Load(file);
                if (!tager.ValidateFileName(mask))
                {
                    badFiles.Add(file);
                }
            }
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("Bad files:");
            foreach (var badFile in badFiles)
            {
                Console.WriteLine(badFile);
            }
            Console.ResetColor();
            Console.WriteLine("Enter 'rename' to rename it");
            Console.WriteLine("Enter 'ignore' to exit");
            var userChoice = Console.ReadLine().ToLower();
            while (userChoice!="rename"||userChoice!="ignore")
            {
                switch (userChoice)
                {
                    case"ignore":
                        return;
                        break;
                    case "rename":
                        foreach (var badFile in badFiles)
                        {
                            tager.Load(badFile);
                            tager.ChangeName(mask);
                        }
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Successfully");
                        Console.ResetColor();
                        Console.WriteLine("Press any key...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Clear();
                        ShowError("Incorrect command.\n Available commands: ignore , rename");
                        Console.ReadKey();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bad files:");
                        foreach (var badFile in badFiles)
                        {
                            Console.WriteLine(badFile);
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter 'rename' to rename it");
                        Console.WriteLine("Enter 'ignore' to exit");
                        userChoice=Console.ReadLine().ToLower();
                        break;
                }
            }
        }

        public static void ShowCurrentFile(IMp3File file)
        {
            if (file == null) return;
            Console.WriteLine("________________");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current file:");
            Console.ResetColor();
            Console.WriteLine(file.Name+".mp3");
            Console.WriteLine("________________");
        }
        public static string GetPath()
        {
            Console.Write("Path:");
            return Environment.CurrentDirectory + @"\Lol.mp3";
            return Console.ReadLine();
        }
        public static string GetMask()
        {
            Console.Write("Mask:");
            return Console.ReadLine();
        }

        public static Mp3Tags GetTagsFromFileName(IMp3File file)
        {
            if(file==null)
                throw new ArgumentException("File is not loaded");
            var fileName = file.Name;
            var mask = new Mask(GetMask());
            var tagValues = Select(mask.GetTagValuesFromString(fileName));
            var result = new Mp3Tags();
            foreach (var tagValue in tagValues)
            {
                result.SetTag(tagValue.Key, tagValue.Value);
            }
            return result;
        }

        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("Press any key...");
            Console.WriteLine("\n\n");
        }

        public static void ShowMenu()
        {
            Console.WriteLine("Available commands:\n1. Load\n2. Save\n3. Changetags\n4. Rename\n5. Analysis\n6. Exit");
        }

        public static void ShowExample()
        {
            Console.Write("Available tags: ");
            foreach (var tag in Enum.GetValues(typeof(TagList)))
            {
                Console.Write(tag+", ");
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
        public static Dictionary<string,string> Select(List<Dictionary<string, string>> tagValuesList)
        {
            Console.WriteLine("Select correct tag values:");
            Dictionary<string, string> result;
            int index = 0;
            do
            {
                Console.Clear();
                result = tagValuesList[index];
                PrintTagValues(result);
                Console.WriteLine("{0}/{1}", index+1, tagValuesList.Count);
                Console.WriteLine("Press enter to select");
                if (++index >= tagValuesList.Count)
                    index = 0;

            } while (Console.ReadKey().Key != ConsoleKey.Enter);
            return result;
        }

        public static void PrintTagValues(Dictionary<string,string> tagValues)
        {
            foreach (var tagValue in tagValues)
            {
                Console.WriteLine("Tag: "+tagValue.Key+" value: "+tagValue.Value);
            }
        }

      
     
    }
}
