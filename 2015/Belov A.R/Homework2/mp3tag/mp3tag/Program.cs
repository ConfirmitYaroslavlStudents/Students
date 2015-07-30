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
                Menu.Show();
                Menu.ShowCurrentFile(tager.CurrentFile);
                switch (Menu.GetUserChoice("\n\nCommand:").ToLower())
                {
                    case "load":
                        if (!tager.Load(Menu.GetUserChoice("path:")))
                        {
                            Menu.ShowError("File does not exist");
                        }
                break;
                    case "changetags":
                        //[TODO] try catch logic in a single place
                        try
                        {
                            Menu.ShowHelp();
                            Menu.ShowCurrentFile(tager.CurrentFile);
                            tager.ChangeTags(GetTagsFromFileName(tager.CurrentFile));
                            tager.Save();
                            Menu.SuccessMessage();
         
                        }
                        catch (Exception e)
                        {
                            Menu.ShowError(e.Message);
                        }
                        break;
                    case "rename":
                        try
                        {
                            Menu.ShowHelp();
                            tager.ChangeName(new Mask(Menu.GetUserChoice("mask:")));
                            Menu.SuccessMessage();
                        }
                        catch (Exception e)
                        {
                            Menu.ShowError(e.Message);
                        }
                        break;
                    case "analysis":
                        try
                        {
                            //AnalyzeFolder(@"C:\Users\Alexandr\Desktop\TEST");
                            AnalyzeFolder(Menu.GetUserChoice("path:"));
                        }
                        catch (Exception e)
                        {

                            Menu.ShowError(e.Message);
                        }
                        break;
                    case "exit":
                        return;
                    default:
                        Menu.ShowError("Error comand");
                        break;
                }
            }
        }

        public static void AnalyzeFolder(string path)
        {
            //[TODO] SRP vialation
            //[TODO] need tests
            var files = Directory.GetFiles(path);
            var tager=new Tager(new FileLoader());
            Menu.ShowHelp();
            var mask=new Mask(Menu.GetUserChoice("mask:"));
            var badFiles=new List<string>();
            foreach (var file in files.Where(file => file.ToLower().EndsWith(".mp3")))
            {
                tager.Load(file);
                if (!tager.ValidateFileName(mask))
                {
                    badFiles.Add(file);
                }
            }
            if (badFiles.Count == 0)
            {
                Menu.ShowMessage("All files is OK");
                return;
            }
            Menu.PrintCollection("Bad files:",badFiles);
            var userChoice = "";
            while (userChoice!="rename"&&userChoice!="ignore")
            {
                userChoice = Menu.GetUserChoice("Enter 'rename' to rename it\nEnter 'ignore' to exit\n").ToLower();
                switch (userChoice)
                {
                    case"ignore":
                        break;
                    case "rename":
                        var errorFiles=new List<string>();
                        foreach (var badFile in badFiles)
                        {
                            try
                            {
                                tager.Load(badFile);
                                tager.ChangeName(mask);
                            }
                            catch (Exception)
                            {
                                
                                errorFiles.Add(badFile);
                            }
                            
                        }
                        if (errorFiles.Count == 0)
                        {
                           Menu.SuccessMessage();
                        }
                        else
                        {
                            Menu.PrintCollection(string.Format("{0} files can't be renamed", errorFiles.Count),errorFiles);
                            Menu.SuccessMessage();
                        }
                        break;
                    default:
                        Menu.ShowError("Incorrect command.\n Available commands: ignore , rename\n");
                        Menu.PrintCollection("Bad files:",badFiles);
                        break;
                }
            }
        }


        public static Mp3Tags GetTagsFromFileName(IMp3File file)
        {
            if(file==null)
                throw new ArgumentException("File is not loaded");
            var fileName = file.Name;
            Menu.ShowHelp();
            var mask = new Mask(Menu.GetUserChoice("mask:"));
            var tagValues = Select(mask.GetTagValuesFromString(fileName));
            var result = new Mp3Tags();
            foreach (var tagValue in tagValues)
            {
                result.SetTag(tagValue.Key, tagValue.Value);
            }
            return result;
        }

        public static Dictionary<string,string> Select(List<Dictionary<string, string>> tagValuesList)
        {
            Menu.ShowInfo("Select correct tag values:");
            Dictionary<string, string> result;
            int index = 0;
            do
            {
                Console.Clear();
                result = tagValuesList[index];
                Menu.PrintTagValues(result);
                Menu.ShowInfo(string.Format("{0}/{1}", index + 1, tagValuesList.Count) + "\nPress enter to select");
                if (++index >= tagValuesList.Count)
                    index = 0;

            } while (Console.ReadKey().Key != ConsoleKey.Enter);
            return result;
        }

       

      
     
    }
}
