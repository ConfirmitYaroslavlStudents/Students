using System;
using System.Collections.Generic;
using System.IO;
using Mp3TagLib;

namespace mp3tager
{
    class Changetags:Operation
    {
        public override void Call()
        {
            var tager = new Tager(new FileLoader());
           // tager.Load(@"C:\Users\Alexandr\Desktop\TEST\1 Holiday.mp3");
            if (!tager.Load(Menu.GetUserInput("path:")))
            {
                throw new FileNotFoundException("File does not exist");
            }
            Menu.PrintHelp();
            Menu.PrintCurrentFile(tager.CurrentFile);
            var tags = GetTagsFromFileName(tager.CurrentFile);
            tager.ChangeTags(tags);
            tager.Save();
            Menu.PrintSuccessMessage();
        }
        Mp3Tags GetTagsFromFileName(IMp3File file)
        {
            if (file == null)
                throw new ArgumentException("File is not loaded");
            Menu.PrintHelp();
            var fileName = file.Name;
            var mask = new Mask(Menu.GetUserInput("mask:"));
            var tagValues = Select(mask.GetTagValuesFromString(fileName));
            var result = new Mp3Tags();
            foreach (var tagValue in tagValues)
            {
                result.SetTag(tagValue.Key, tagValue.Value);
            }
            return result;
        }
        Dictionary<string, string> Select(List<Dictionary<string, string>> tagValuesList)
        {
            Menu.PrintMessage("Select correct tag values:");
            Dictionary<string, string> result;
            var index = 0;
            do
            {
                Console.Clear();
                result = tagValuesList[index];
                Menu.PrintTagValues(result);
                Menu.PrintMessage(string.Format("{0}/{1}", index + 1, tagValuesList.Count) + "\n\t\tPress enter to select\n\t\tPress any key to scroll");
                if (++index >= tagValuesList.Count)
                    index = 0;

            } while (Console.ReadKey().Key != ConsoleKey.Enter);
            return result;
        }
    }
}
