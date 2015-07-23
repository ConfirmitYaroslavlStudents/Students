using System;
using System.Collections.Generic;
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
                switch (Console.ReadLine().ToLower())
                {
                    case "load":
                        Console.Clear();
                        if (!tager.Load(GetPath()))
                        {
                            ShowError("File does not exist");
                            Console.ReadKey();
                        }
                break;
                    case "save":
                        Console.Clear();
                        if(!tager.Save())
                            ShowError("File is not loaded");
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Successfully");
                            Console.ResetColor();
                            Console.WriteLine("Press any key...");
                        }
                        Console.ReadKey();
                        break;
                    case "changetags":
                        try
                        {
                            Console.Clear();
                            ShowExample();
                            ShowCurrentFile(tager.CurrentFile);
                            {tager.ChangeTags(GetTagsFromFileName(tager.CurrentFile));}
         
                        }
                        catch (Exception e)
                        {
                            Console.Clear();
                            ShowError(e.Message);
                            Console.ReadKey();
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

        public static void ShowCurrentFile(IMp3File file)
        {
            if (file == null) return;
            Console.WriteLine("________________");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current file:");
            Console.ResetColor();
            var lastSlashIndex = file.Name.LastIndexOf(@"\");
            Console.WriteLine(file.Name.Substring(lastSlashIndex + 1, file.Name.Length - lastSlashIndex - 1));
            Console.WriteLine("________________");
        }
        public static string GetPath()
        {
            Console.Write("Path:");
           // return Environment.CurrentDirectory + @"\[Parental Advisory Explicit Guitar]CROW'SCLAW - 200kmh (Instrumental).mp3";
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
            var lastSlashIndex = fileName.LastIndexOf(@"\");
            fileName = fileName.Substring(lastSlashIndex + 1,
                fileName.Length - lastSlashIndex - 1 - ".mp3".Length);
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
            Console.WriteLine("Available commands:\n1. Load\n2. Save\n3. Changetags\n4. Exit");
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
